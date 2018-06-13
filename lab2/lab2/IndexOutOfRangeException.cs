using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    internal class IndexOutOfRangeException : My_Exeption
    {
        public IndexOutOfRangeException(string message)
            : base(message)
        {
        }
    }
}
