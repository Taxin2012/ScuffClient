using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScuffClient.Reflections;

namespace ScuffClient.Photon
{
    public class ActorProperties
    {
        public static KeyValuePair<object, object> SetModTag(string tag) => new KeyValuePair<object, object>("modTag", tag);
        public static KeyValuePair<object, object> SetIsInvisible(bool invis) => new KeyValuePair<object, object>("isInvisible", invis);
        public static KeyValuePair<object, object> SetAvatarVariations(string id) => new KeyValuePair<object, object>("avatarVariations", id);
        public static KeyValuePair<object, object> SetStatus(string message) => new KeyValuePair<object, object>("status", message);
        public static KeyValuePair<object, object> SetStatusDescription(string description) => new KeyValuePair<object, object>("statusDescription", description);
        public static KeyValuePair<object, object> SetIsInVR() => new KeyValuePair<object, object>("inVRMode", VRCTrackingManager.IsInVRMode());
        public static KeyValuePair<object, object> SetShowSocialRank(string rank) => new KeyValuePair<object, object>("showSocialRank", PlayerReflections.GetShowSocialRank());
        public static KeyValuePair<object, object> SetAvatarId(string id) => new KeyValuePair<object, object>("avatarId", id);
        public static KeyValuePair<object, object> SetSteamUserID(string steamId) => new KeyValuePair<object, object>("steamUserID", steamId);
    }
}
