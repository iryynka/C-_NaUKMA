using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4.Exeptions
{
    internal class AgeInPast : Exception
    {
        public AgeInPast(string message) : base(message)
        {
        }
    }
}
