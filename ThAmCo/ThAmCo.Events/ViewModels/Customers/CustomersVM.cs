using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThAmCo.Events.ViewModels.Customers
{
    public class CustomersVM
    {
        public CustomersVM(List<Data.Customer> customers)
        {
            Customers = customers;
        }

        public List<Data.Customer> Customers { get; set; }
    }
}
