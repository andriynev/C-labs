using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Register memory = new Register
            {
                new RSTrigger(),
                new JKTrigger(),
                new RSTrigger(),
                new RSTrigger(),
                new JKTrigger()
            };

            
            memory[0].ComputeOutputSignals(true, false);
            memory[1].ComputeOutputSignals(true, false);
            memory[2].ComputeOutputSignals(false, false);
            memory[3].ComputeOutputSignals(false, true);
            memory[4].ComputeOutputSignals(true, true);
            foreach (var item in memory)
            {
                Console.WriteLine(item.GetInfo());
            }


            Register reg = new Register();
            reg = reg.DeepCopy(memory);

            Console.WriteLine();
            foreach (var item in reg)
            {
                Console.WriteLine(item.GetInfo());
            }


            Console.WriteLine();
            if (memory[0] == memory[1])
                Console.WriteLine("memory[0] == memory[1]");
            else
                Console.WriteLine("memory[0] != memory[1]");

            Console.ReadKey();
        }
    }
}
