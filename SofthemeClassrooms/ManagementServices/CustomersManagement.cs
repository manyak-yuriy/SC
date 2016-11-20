using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;



namespace ManagementServices
{
    public static class CustomersManagement
    {
        public static List<Customers> getAllCustomers()
        {
            var context = new MyDBContext();
            List<Customers> customers = context.Customers.ToList();
            return customers;
        }
    }
}
