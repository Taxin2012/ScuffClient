using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScuffClient.Photon
{
    public class ParameterCodes
    {
        public const byte Broadcast = 250;
        public const byte EventCode = 244;
        public const byte CustomData = 245;
        public const byte Properties = 251;
        public const byte GameProperties = 248;
        public const byte PlayerProperties = 249;
        public const byte TargetActor = 254;
        public const byte Targets = 252;
        public const byte WorldId = 255;
        /// <summary>
        /// (byte)1 should always be used with JoinMode
        /// </summary>
        public const byte JoinMode = 215;
        public const byte LobbyName = 213;
        /// <summary>
        /// (byte)0 should always be used with LobbyType
        /// </summary>
        public const byte LobbyType = 212;
        public const byte CleanupCacheOnLeave = 241;
        public const byte CheckUserOnJoin = 232;
        public const byte AppVersion = 220;
        public const byte AppId = 224;
        /// <summary>
        /// Should always be "usw"
        /// </summary>
        public const byte Region = 210;
        public const byte ClientAuthenticationType = 217;
        /// <summary>
        /// token=cookieANDuser=userId
        /// </summary>
        public const byte ClientAuthenticationParameters = 216;
        public const byte EncryptionKey = 221;

    }
}
