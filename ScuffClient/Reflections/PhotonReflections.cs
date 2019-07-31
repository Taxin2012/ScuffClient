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
        private static FieldInfo fi_LoadBalancingClient;
        private static Type t_LoadBalancingPeerType;
        private static MethodInfo m_LoadBalancingPeerMethod;
        private static Type t_PhotonNetwork;
        private static List<Type> list_Types;
        private static MethodInfo m_sendOperation;

        public static object GetLoadBalancingPeer()
        {
            return m_LoadBalancingPeerMethod.Invoke(GetLoadBalancingClient(), null);
        }
        public static object GetLoadBalancingClient()
        {
            return fi_LoadBalancingClient.GetValue(null);
        }

        static PhotonReflections()
        {
            list_Types = AppDomain.CurrentDomain.GetAssemblies().First((Assembly a) => a.GetName().Name == "Assembly-CSharp").GetTypes().ToList<Type>();
            t_PhotonNetwork = list_Types.First(delegate (Type t)
            {
                if (t.IsAbstract && t.IsPublic)
                {
                    return (from f in t.GetFields()
                            where f.FieldType == typeof(ServerSettings)
                            select f).Count<FieldInfo>() == 1;
                }
                return false;
            });
            fi_LoadBalancingClient = t_PhotonNetwork.GetFields().First((FieldInfo f) => f.IsPublic && f.IsStatic && typeof(IPhotonPeerListener).IsAssignableFrom(f.FieldType));
            t_LoadBalancingPeerType = list_Types.First(delegate (Type t)
            {
                if (t.IsPublic && !t.IsAbstract && typeof(PhotonPeer).IsAssignableFrom(t))
                {
                    return t.GetFields(BindingFlags.Static | BindingFlags.NonPublic).Any((FieldInfo f) => f.FieldType == typeof(Type));
                }
                return false;
            });
            m_LoadBalancingPeerMethod = fi_LoadBalancingClient.FieldType.GetProperties(BindingFlags.Instance | BindingFlags.Public).First((PropertyInfo p) => p.PropertyType == t_LoadBalancingPeerType).GetGetMethod();
            m_sendOperation = m_LoadBalancingPeerMethod.ReturnType.GetMethods().First(delegate (MethodInfo m)
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
            m_sendOperation.Invoke(GetLoadBalancingPeer(), new object[]
                {
                    opCode,
                    parameters,
                    options
                });
        }
    }
}
