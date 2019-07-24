using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ExitGames.Client.Photon;
using Photon.Pun;

namespace ScuffClient.Reflections
{
    public class PhotonReflections
    {
        private static FieldInfo getLoadBalancingClient;
        private static Type getLoadBalancingPeerType;
        private static MethodInfo getLoadBalancingPeerMethod;
        private static Type getPhotonNetwork;
        private static List<Type> getTypes;
        public static MethodInfo sendOperation;

        public static object GetLoadBalancingPeer()
        {
            return getLoadBalancingPeerMethod.Invoke(GetLoadBalancingClient(), null);
        }
        public static object GetLoadBalancingClient()
        {
            return getLoadBalancingClient.GetValue(null);
        }

        static PhotonReflections()
        {
            getTypes = AppDomain.CurrentDomain.GetAssemblies().First((Assembly a) => a.GetName().Name == "Assembly-CSharp").GetTypes().ToList<Type>();
            getPhotonNetwork = getTypes.First(delegate (Type t)
            {
                if (t.IsAbstract && t.IsPublic)
                {
                    return (from f in t.GetFields()
                            where f.FieldType == typeof(ServerSettings)
                            select f).Count<FieldInfo>() == 1;
                }
                return false;
            });
            getLoadBalancingClient = getPhotonNetwork.GetFields().First((FieldInfo f) => f.IsPublic && f.IsStatic && typeof(IPhotonPeerListener).IsAssignableFrom(f.FieldType));
            getLoadBalancingPeerType = getTypes.First(delegate (Type t)
            {
                if (t.IsPublic && !t.IsAbstract && typeof(PhotonPeer).IsAssignableFrom(t))
                {
                    return t.GetFields(BindingFlags.Static | BindingFlags.NonPublic).Any((FieldInfo f) => f.FieldType == typeof(Type));
                }
                return false;
            });
            getLoadBalancingPeerMethod = getLoadBalancingClient.FieldType.GetProperties(BindingFlags.Instance | BindingFlags.Public).First((PropertyInfo p) => p.PropertyType == getLoadBalancingPeerType).GetGetMethod();
            sendOperation = getLoadBalancingPeerMethod.ReturnType.GetMethods().First(delegate (MethodInfo m)
            {
                if (m.Name == "SendOperation")
                {
                    return m.GetParameters().Any((ParameterInfo p) => p.ParameterType == typeof(SendOptions));
                }
                return false;
            });
        }
        public static void SendOperation(byte opCode, Dictionary<byte, object> parameters, SendOptions options)
        {
            sendOperation.Invoke(GetLoadBalancingPeer(), new object[]
                {
                    opCode,
                    parameters,
                    options
                });
        }
    }
}
