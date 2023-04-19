using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4.Exeptions
{
    internal class WrongEmail : Exception
    {
        public WrongEmail(string message) : base(message)
        {
        }
    }
}
