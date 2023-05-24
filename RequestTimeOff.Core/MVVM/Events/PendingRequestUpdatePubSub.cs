using RequestTimeOff.MVVM.Events;
using RequestTimeOff.MVVM;
using System;
using System.Collections.Generic;
using System.Text;

namespace RequestTimeOff.Core.MVVM.Events
{
    internal class PendingRequestUpdatePubSub : PubSub<PendingRequestUpdate>
    {
        public static PendingRequestUpdatePubSub Instance { get; } = new PendingRequestUpdatePubSub();
    }
    public class PendingRequestUpdate
    {

    }
}
