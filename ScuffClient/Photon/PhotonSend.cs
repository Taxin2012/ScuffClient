using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using ScuffClient.Reflections;

namespace ScuffClient.Photon
{
    public class PhotonSend
    {
        public static void RaiseEvent(byte eventCode, object eventData, RaiseEventOptions eventOptions, SendOptions options)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>();
            parameters[ParameterCodes.EventCode] = eventCode;
            //check for correct type for eventdata relative to eventcode or else dont send (due to bans)
            if (eventData != null && CheckForCorrectType(eventCode, eventData))
                parameters[ParameterCodes.CustomData] = eventData;
            else
            {
                //print expected type for eventcode
                Console.WriteLine(string.Concat(new object[]
                    {
                        "Invalid CustomData type for eventCode: ",
                        eventCode,
                        " Expected: ",
                        GetCorrectType(eventCode)
                    }));
                return;
            }
            if(eventOptions.TargetActors != null)
                parameters[ParameterCodes.Targets] = eventOptions.TargetActors;

            PhotonReflections.SendOperation(OpCodes.RaiseEvent, parameters, options);
        }

        public static void SetProperties(int actor, KeyValuePair<object, object>[] pair)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>();
            Hashtable properties = new Hashtable();
            //add each array element to hashtable to send as properties
            foreach (KeyValuePair<object, object> kp in pair)
                properties.Add(kp.Key, kp.Value);

            parameters[ParameterCodes.Properties] = properties;
            parameters[ParameterCodes.Broadcast] = true;
            parameters[ParameterCodes.TargetActor] = actor;

            PhotonReflections.SendOperation(OpCodes.SetProperties, parameters, SendOptions.SendReliable);
        }

        public static void Authenticate(string releaseVersion, string appId, string region, string userId, string authCookie, string encryptionKey)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                [ParameterCodes.AppVersion] = releaseVersion,
                [ParameterCodes.AppId] = appId,
                [ParameterCodes.Region] = "usw",
                [ParameterCodes.ClientAuthenticationType] = 0,
                [ParameterCodes.ClientAuthenticationParameters] = string.Concat(new object[]
                {
                    "token=",
                    authCookie,
                    "&user",
                    userId
                }),
                //TODO: Figure out how encryption key is generated
                [ParameterCodes.EncryptionKey] = "??"
            };

            PhotonReflections.SendOperation(230, parameters, new SendOptions()
            {
                Reliability = true,
                Encrypt = true
            });
        }

        private static bool CheckForCorrectType(byte evCode, object type)
        {
            if (evCode == EventType.SendVoice && type is byte[])
                return true;

            if (evCode == EventType.SendEvent && type is VRC_EventLog)
                return true;

            if (evCode == EventType.SendSerialize && type is object[])
                return true;

            if (evCode == EventType.SendPosition)
                return false;

            return false;
        }

        private static string GetCorrectType(byte evCode)
        {
            if (evCode == EventType.SendVoice)
                return "byte[]";
            if (evCode == EventType.SendEvent)
                return "VRC_EventLog";
            if (evCode == EventType.SendSerialize)
                return "unknown";
            if (evCode == EventType.SendPosition)
                return "unknown";
            return "unknown";
        }
    }
}
