using Harmony;
using System;
using VRC;
using VRC.Core;
using ExitGames.Client.Photon;
using System.Reflection;
using UnityEngine;
using ScuffClient.Reflections;
using System.Collections.Generic;
using ScuffClient.Photon;
using ScuffClient.Misc;

namespace ScuffClient.Patches
{
    public class Patcher
    {
        /*
         * Harmony Useage:
         * ref __result in parameters to modify the return value of the method
         * ref __i where i is the index of the parameter you want to change
         * ref __instance to get the instance of the invoked method
         * return false to not run the original method and only run the modified one
         * return true to run the original method after the modified one
         */
        private static HarmonyInstance instance;

        public static void CreatePatches()
        {
            try
            {
                instance = HarmonyInstance.Create("PatcherInstance");
                instance.Patch(typeof(ModerationManager).GetMethod("IsBlockedEitherWay"), FindPatch("NoBlock"), null, null);
                instance.Patch(typeof(ModerationManager).GetMethod("IsKicked"), FindPatch("NoKick"), null, null);
                instance.Patch(typeof(ModerationManager).GetMethod("IsKickedFromWorld"), FindPatch("NoKick"), null, null);
                instance.Patch(typeof(ModerationManager).GetMethod("KickUserRPC", BindingFlags.NonPublic | BindingFlags.Instance), FindPatch("NoKickUser"), null, null);
                instance.Patch(typeof(API).GetMethod("get_DeviceID"), FindPatch("RandomHwid"), null, null);
                instance.Patch(typeof(PortalTrigger).GetMethod("OnTriggerEnter", BindingFlags.Instance | BindingFlags.NonPublic), FindPatch("AntiPortalPatch"), null, null);
                instance.Patch(typeof(PhotonPeer).GetMethod("SendOperation", BindingFlags.Instance | BindingFlags.Public), FindPatch("SteamSpoof"), null, null);
                instance.Patch(typeof(PhotonPeer).GetMethod("SendOperation", BindingFlags.Instance | BindingFlags.Public), FindPatch("GetSendOperationData"), null, null);
                instance.Patch(typeof(USpeakPhotonSender3D).GetMethod("OnEvent", BindingFlags.Instance | BindingFlags.Public), FindPatch("LogEvent"), null, null);
            }
            catch(Exception e)
            {
                Console.WriteLine("[ScuffClient]: Exception caught while patching " + e);
            }
        }

        private static bool NoBlock(ref bool __result, ref ModerationManager __instance, ref string __0)
        {
            Player sender = PlayerManager.GetPlayer(__0);

            if (__instance.IsBlocked(__0) || __instance.IsBlockedByUser(__0))
                sender.vrcPlayer.SetNamePlateColor(Variables.blockedColor);
            else
                sender.vrcPlayer.RestoreNamePlateColor();

            __result = false; //I am not blocked
            return false;
        }

        private static bool NoKick(ref bool __result)
        {
            __result = false;
            return false;
        }

        private static bool NoKickUser(ref string __0, ref Player __4)
        {
            if (__0 == APIUser.CurrentUser.id)
                MiscReflections.GetUiManagerInstance().QueueHudMessage(__4.ToString() + " tried to kick you.");
            return false;
        }

        private static bool AntiPortalPatch()
        {
            return Variables.antiPortal;
        }

        private static bool RandomHwid(ref string __result)
        {
            __result = Functions.NewDeviceID();
            return false;
        }

        private static bool SteamSpoof(byte __0, Dictionary<byte, object> __1)
        {
            void RemoveSteamId(byte key)
            {
                Hashtable data = (Hashtable)__1[key];
                if (data.ContainsKey("steamUserID"))
                    data["steamUserID"] = !string.IsNullOrEmpty(Variables.steamId) ? Variables.steamId : "0";
            }
            if(__0 == OpCodes.SetProperties)
            {
                if (!__1.ContainsKey(ParameterCodes.Properties) || !(__1[ParameterCodes.Properties] is Hashtable))
                    return true;

                RemoveSteamId(ParameterCodes.Properties);
            }
            else if(__0 == OpCodes.JoinGame)
            {
                if (!__1.ContainsKey(ParameterCodes.PlayerProperties) || !(__1[ParameterCodes.PlayerProperties] is Hashtable))
                    return true;

                RemoveSteamId(ParameterCodes.PlayerProperties);
            }
            return true;
        }

        private static bool GetSendOperationData(byte __0, Dictionary<byte, object> __1, SendOptions __2)
        {
            return true;
        }

        private static bool LogEvent(EventData __0)
        {
            return true;
        }

        private static HarmonyMethod FindPatch(string method)
        {
            return new HarmonyMethod(typeof(Patcher).GetMethod(method, BindingFlags.Static | BindingFlags.NonPublic));
        }
    }
}
