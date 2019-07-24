using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScuffClient.Photon
{
    public class RaiseEventOptions
    {
        public int[] TargetActors;
        //public byte EventCode;
        public CachingOptions Caching;
        public Recievers Recievers;

    }
    public enum CachingOptions : byte
    {
        DoNotCache,
        [Obsolete]
        MergeCache,
        [Obsolete]
        ReplaceCache,
        [Obsolete]
        RemoveCache,
        AddToRoomCache,
        AddToRoomCacheGlobal,
        RemoveFromRoomCache,
        RemoveFromRoomCacheForActorsLeft,
        SliceIncreaseIndex = 10,
        SliceSetIndex,
        SlicePurgeIndex,
        SlicePurgeUpToIndex
    }
    public enum Recievers : byte
    {
        Others,
        All,
        MasterClient
    }
}
