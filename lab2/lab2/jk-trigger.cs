using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    public class JKTrigger : Trigger
    {
        public JKTrigger()
        {
            Type = TriggerType.JKTrigger;
        }

        public override void ComputeOutputSignals(bool j, bool k)
        {
            Input1 = j;
            Input2 = k;
            if (!j && !k)
            {
                return;
            }

            if (k)
            {
                CurrentQ = false;
            }

            if (j)
            {
                CurrentQ = true;
            }
            if (j && k)
            {
                CurrentQ = !CurrentQ;
            }
        }

        public override Trigger DeepCopy(Trigger t)
        {
            JKTrigger tr = new JKTrigger();
            tr.CurrentQ = t.CurrentQ;
            tr.Input1 = t.Input1;
            tr.Input2 = t.Input2;
            return tr;
        }
    }
}
