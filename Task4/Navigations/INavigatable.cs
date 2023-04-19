using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task4.ViewModels;

namespace Task4.Navigations
{
    internal interface INavigatable
    {
        NavigationTypes ViewType
        {
            get;
        }
    }
}
