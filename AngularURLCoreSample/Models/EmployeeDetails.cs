using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularwithASPCore.Models
{
    public class Employee1Details
    {
        public static List<Employee1Details> order = new List<Employee1Details>();
        public Employee1Details()
        {

        }
        public Employee1Details(int OrderID, string FirstName, string LastName, int Amount)
        {
            this.OrderID = OrderID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Amount = Amount;
        }
        public static List<Employee1Details> GetAllRecords()
        {
            if (order.Count() == 0)
            {
                int code = 10000;
                for (int i = 1; i < 2; i++)
                {
                    order.Add(new Employee1Details(code + 1, "Nancy", "Davolio", 500));
                    order.Add(new Employee1Details(code + 2, "Andrew", "Fuller", 100));
                    order.Add(new Employee1Details(code + 3, "Janet", "Leverling", 900));
                    order.Add(new Employee1Details(code + 4, "Margaret", "Peacock", 200));
                    order.Add(new Employee1Details(code + 5, "John", "Dev", 650));
                    code += 5;
                }
            }
            return order;
        }


        public int? OrderID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Amount { get; set; }
    }
}
