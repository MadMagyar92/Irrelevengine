 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Completely_Irrelevant
{
    
    public class BasicItemSet : ItemSet
    {
        public BasicItemSet(InvokingMode mode)
            : base(mode)
        {

        }

        public BasicItemSet(InvokingMode mode, List<ISender> senders, List<IReceiver> receivers)
            : base(mode)
        {
            RegisterSender(senders);
            RegisterReceiver(receivers);
        }
    }
}
