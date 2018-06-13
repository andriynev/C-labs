using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    public class RSTrigger : Trigger
    {

        public RSTrigger()
        {
            Type = TriggerType.RSTrigger;
        }

        public override void ComputeOutputSignals(bool set, bool reset)
        {
            if (set && reset)
            {
                throw new ArgumentException("Invalid input signals. Given set = 1 and reset = 1");
            }
            Input1 = set;
            Input2 = reset;
            if (!set && !reset)
            {
                return;
            }

            if (reset) {
                CurrentQ = false;
            }

            if (set)
            {
                CurrentQ = true;
            }
        }

        public override Trigger DeepCopy(Trigger t)
        {
            RSTrigger tr = new RSTrigger();
            tr.CurrentQ = t.CurrentQ;
            tr.Input1 = t.Input1;
            tr.Input2 = t.Input2;
            return tr;
        }
    }
}
