using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4.Exeptions
{
    internal class AgeInFuture : Exception
    {
        public AgeInFuture(string message) : base(message)
        {
        }
    }
}
