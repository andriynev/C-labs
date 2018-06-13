using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    internal class My_Exeption : Exception
    {
        public My_Exeption(string message)
            : base(message)
        {
        }
    }
}
