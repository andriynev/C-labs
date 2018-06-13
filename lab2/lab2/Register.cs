using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    public class Register: IEnumerable<Trigger>
    {
        private List<Trigger> memory = new List<Trigger>();

        public void Reset()
        {
            foreach (var item in memory)
            {
                item.ComputeOutputSignals(false, false);
            }

            
        }

        public void Add(Trigger trigger)
        {
            memory.Add(trigger);
        }

        

        public Register DeepCopy(Register obj)
        {
            if (obj == null)
                throw new ArgumentNullException("Object cannot be null");
            Register reg = new Register();
            foreach (var item in obj)
            {
                reg.Add(item.DeepCopy(item));
            }
            return reg;
        }

        public IEnumerator<Trigger> GetEnumerator()
        {
            return memory.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return memory.GetEnumerator();
        }

        public Trigger this[int index]    // Indexer declaration  
        {
            get { return memory[index]; }
            set { memory[index] = value; }
        }
    }
}
