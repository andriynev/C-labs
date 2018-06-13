using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    public enum TriggerType
    {
        Unknown,
        RSTrigger,
        JKTrigger
    }
    public struct TriggerInfo
    {
        public TriggerType Type;
        public bool CurrentQ;
        public TriggerInfo(TriggerType type, bool currentQ)
        {
            Type = type;
            CurrentQ = currentQ;
        }

        public override string ToString()
        {
            return String.Format("Trigger type {0}; Current output signal {1}", Type, CurrentQ);
        }
    }

    public abstract class Trigger
    {
        public TriggerType Type { get; set; }
        public bool Input1 { get; protected set;}
        public bool Input2 { get; protected set; }
        public bool CurrentQ { get; protected set; }

        public abstract void ComputeOutputSignals(bool in1, bool in2);

        public TriggerInfo GetInfo()
        {
            return new TriggerInfo(Type, CurrentQ);
        }

        public override bool Equals(object obj)
        {
            // Check for null values and compare run-time types.
            if (obj == null || GetType() != obj.GetType())
                return false;
            Trigger t = (Trigger)obj;
            return (Type == t.Type) && (CurrentQ == t.CurrentQ);
        }

        public override int GetHashCode()
        {
            var hashCode = -1953566921;
            hashCode = hashCode * -1521134295 + (int)Type;
            hashCode = hashCode * -1521134295 + Convert.ToInt16(Input1);
            hashCode = hashCode * -1521134295 + Convert.ToInt16(Input2);
            hashCode = hashCode * -1521134295 + Convert.ToInt16(CurrentQ);
            return hashCode;
        }

        public static bool operator ==(Trigger x, Trigger y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(Trigger x, Trigger y)
        {
            return !x.Equals(y);
        }

        public abstract Trigger DeepCopy(Trigger t);
    }
}
