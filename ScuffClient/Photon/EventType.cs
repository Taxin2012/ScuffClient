using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScuffClient.Photon
{
    public class EventType
    {
        public const byte SendVoice = 1;
        public const byte SendEvent = 6;
        public const byte SendSerialize = 201;
        public const byte SendPosition = 202;
        public const byte SendTest = 206;
        public const byte SendTransferPv = 210;
    }
}
