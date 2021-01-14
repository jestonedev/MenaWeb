using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MenaWeb.ViewOptions;

namespace MenaWeb.ViewModels
{
    public class ListVM<F> where F : FilterOptions, new()
    {
        public ListVM()
        {
            OrderOptions = new OrderOptions();
            PageOptions = new PageOptions();
            FilterOptions = new F();
        }

        public OrderOptions OrderOptions { get; set; }
        public PageOptions PageOptions { get; set; }
        public F FilterOptions { get; set; }
    }
}
