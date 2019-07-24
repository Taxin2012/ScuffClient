using System.Reflection;
using VRC;
using VRC.Core;
using VRCSDK2;
using Player = VRC.Player;

namespace ScuffClient.Reflections
{
    public class PlayerReflections
    {
        #region Player Reflections
        private readonly static MethodInfo m_IsBlockedEitherWay = typeof(Player).GetMethod("get_isBlockedEitherWay");
        private readonly static MethodInfo m_PlayerNet = typeof(Player).GetMethod("get_playerNet");
        private readonly static MethodInfo m_ApiUserMethod = typeof(Player).GetMethod("get_user");
        private readonly static MethodInfo m_PlayerApiMethod = typeof(Player).GetMethod("get_playerApi");
        private readonly static MethodInfo m_UserIdMethod = typeof(Player).GetMethod("get_userId");
        private readonly static MethodInfo m_PhotonIdMethod = typeof(Player).GetMethod("get_PhotonPlayer");
        private readonly static MethodInfo m_IsDevMethod = typeof(Player).GetMethod("get_isSuper");
        private readonly static MethodInfo m_IsModMethod = typeof(Player).GetMethod("get_isModerator");
        private readonly static MethodInfo m_IsCurrentMethod = typeof(Player).GetMethod("get_isLocal");
        private readonly static MethodInfo m_IsFriendMethod = typeof(Player).GetMethod("get_isFriend");
        #endregion
        #region VRCPlayer Reflections
        private readonly static MethodInfo m_USpeakerMethod = typeof(VRCPlayer).GetMethod("get_uSpeaker");
        private readonly static MethodInfo m_PlayerMethod = typeof(VRCPlayer).GetMethod("get_player");
        private readonly static MethodInfo m_AvatarApiMethod = typeof(VRCPlayer).GetMethod("get_AvatarModel");
        private readonly static MethodInfo m_SteamMethod = typeof(VRCPlayer).GetMethod("get_SteamUserIDULong");
        #endregion
        #region APIUser Reflections
        private readonly static MethodInfo m_ShowSocialRank = typeof(APIUser).GetMethod("get_showSocialRank");
        #endregion


        public static APIUser GetApiUser(Player player) => (APIUser)m_ApiUserMethod.Invoke(player, null);
        public static VRC_PlayerApi GetPlayerApi(Player player) => (VRC_PlayerApi)m_PlayerApiMethod.Invoke(player, null);
        public static string GetUserId(Player player) => (string)m_UserIdMethod.Invoke(player, null);
        public static bool GetIsDev(Player player) => (bool)m_IsDevMethod.Invoke(player, null);
        public static bool GetIsMod(Player player) => (bool)m_IsModMethod.Invoke(player, null);
        public static bool GetIsFriend(Player player) => (bool)m_IsFriendMethod.Invoke(player, null);
        public static bool GetIsCurrent(Player player) => (bool)m_IsCurrentMethod.Invoke(player, null);
        public static bool GetIsBlockedEitherWay(Player player) => (bool)m_IsBlockedEitherWay.Invoke(player, null);
        public static bool GetShowSocialRank() => (bool)m_ShowSocialRank.Invoke(APIUser.CurrentUser, null);
        public static ApiAvatar GetApiAvatar(Player player) => (ApiAvatar)m_AvatarApiMethod.Invoke(player.vrcPlayer, null);
        public static ulong GetSteamId(Player player) => (ulong)m_SteamMethod.Invoke(player.vrcPlayer, null);

    }
}
