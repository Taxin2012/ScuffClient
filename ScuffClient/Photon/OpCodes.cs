using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScuffClient.Photon
{
    public class OpCodes
    {
        public const byte JoinGame = 226;
        public const byte CreateLobby = 227;
        public const byte Authentication = 230;
        public const byte SetProperties = 252;
        public const byte RaiseEvent = 253;
        public const byte Leave = 254;
    }
}
