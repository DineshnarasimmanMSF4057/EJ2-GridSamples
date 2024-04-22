using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreGridApp.Models;
using Microsoft.AspNetCore.Cors;
using Syncfusion.EJ2.Base;
using System.Linq;

namespace CoreGridApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Orders")]
    public class OrdersController : Controller
    {
        // GET: api/Orders
        [HttpGet]

    
        public object Get()
        {
            
            var queryString = Request.Query;
            var data = OrdersDetails.GetAllRecords().ToList();
            int skip = Convert.ToInt32(queryString["$skip"]);
            int take = Convert.ToInt32(queryString["$top"]);
            string sort = queryString["$orderby"];   // get the sort queries 
           
            string filter = queryString["$filter"];  //filtering
            if(sort!=null)
            {
                bool desc = sort.Split(" ").Contains("desc");
                string descSot = sort.Split(" ")[0];
                if(desc)
                {
                    if (descSot == "OrderID")
                    {
                        data = data.OrderByDescending(x => x.OrderID).ToList(); ;
                    }
                    if (descSot == "EmployeeID")
                    {
                        data = data.OrderByDescending(x => x.EmployeeID).ToList(); ; ;
                    }
                    if (descSot == "CustomerID")
                    {
                        data = data.OrderByDescending(x => x.CustomerID).ToList(); ;
                    }
                    if (descSot == "ShipCity")
                    {
                        data = data.OrderByDescending(x => x.ShipCity).ToList(); ;
                    }
                    if (descSot == "Verified")
                    {
                        data = data.OrderByDescending(x => x.Verified).ToList(); ;
                    }
                    if (descSot == "ShipCountry")
                    {
                        data = data.OrderByDescending(x => x.ShipCountry).ToList(); ;
                    }
                    if (descSot == "Freight")
                    {
                        data = data.OrderByDescending(x => x.Freight).ToList(); ;
                    }

                }
                else
                {
                    if (sort == "OrderID")
                    {
                        data = data.OrderBy(x => x.OrderID).ToList(); ;
                    }
                    if (sort == "EmployeeID")
                    {
                        data = data.OrderBy(x => x.EmployeeID).ToList(); ; ;
                    }
                    if (sort == "CustomerID")
                    {
                        data = data.OrderBy(x => x.CustomerID).ToList(); ;
                    }
                    if (sort == "ShipCity")
                    {
                        data = data.OrderBy(x => x.ShipCity).ToList(); ;
                    }
                    if (sort == "Verified")
                    {
                        data = data.OrderBy(x => x.Verified).ToList(); ;
                    }
                    if (sort == "ShipCountry")
                    {
                        data = data.OrderBy(x => x.ShipCountry).ToList(); ;
                    }
                    if (sort == "Freight")
                    {
                        data = data.OrderBy(x => x.Freight).ToList(); ;
                    }
                }
                
                

            }
            if (filter != null) // to handle filter opertaion
            {
                if (filter.Contains("substring"))//searching 
                {
                    var key = filter.Split(new string[] { "'" }, StringSplitOptions.None)[1];
                    data = data.Where(fil => fil.CustomerID.ToLower().ToString().Contains(key.ToLower())
                                            || fil.EmployeeID.ToString().Contains(key)
                                            || fil.ShipCity.ToLower().Contains(key.ToLower())
                                            || fil.OrderID.ToString().Contains(key)).ToList();
                }
                else
                {
                    var newfiltersplits = filter;
                    var filtersplits = newfiltersplits.Split('(', ')', ' ');
                    var filterfield = filtersplits[1];
                    var filtervalue = filtersplits[3];

                    if (filtersplits.Length == 5)
                    {
                        if (filtersplits[1] == "tolower")
                        {
                            filterfield = filter.Split('(', ')', '\'')[2];
                            filtervalue = filter.Split('(', ')', '\'')[4];
                        }
                    }

                    if (filtersplits.Length != 5)
                    {
                        filterfield = filter.Split('(', ')', '\'')[3];
                        filtervalue = filter.Split('(', ')', '\'')[5];

                    }

                    if (filterfield == "OrderID")
                    {
                        data = (from cust in data
                                where cust.OrderID.ToString() == filtervalue.ToString()
                                select cust).ToList();
                    }
                    if (filterfield == "EmployeeID")
                    {
                        data = (from cust in data
                                where cust.EmployeeID.ToString() == filtervalue.ToString()
                                select cust).ToList();
                    }
                    if (filterfield == "CustomerID")
                    {
                        data = (from cust in data
                                where cust.CustomerID.ToLower().StartsWith(filtervalue.ToString())
                                select cust).ToList();
                    }
                    if (filterfield == "ShipCountry")
                    {
                        data = (from cust in data
                                where cust.ShipCountry.ToLower().StartsWith(filtervalue.ToString())
                                select cust).ToList();
                    }
                    if (filterfield == "Freight")
                    {
                        data = (from cust in data
                                where cust.Freight.ToString() == filtervalue.ToString()
                                select cust).ToList();
                    }
                    if (filterfield == "ShipCity")
                    {
                        data = (from cust in data
                                where cust.ShipCity.ToLower().StartsWith(filtervalue.ToString())
                                select cust).ToList();
                    }
                    if (filterfield == "Verified")
                    {
                        data = (from cust in data
                                where cust.Verified == bool.Parse(filtervalue.ToString())
                                select cust).ToList();
                    }
                }
            }
            return take != 0 ? new { result = data.Skip(skip).Take(take).ToList(), count = data.Count() } : new { result = data, count = data.Count() };
        }

   
        // GET: api/Orders/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Orders
        [HttpPost]
        public object Post([FromBody]OrdersDetails value)
        {
            OrdersDetails.GetAllRecords().Insert(0, value);
            var data = OrdersDetails.GetAllRecords().ToList();
            return Json(new { result = data, count = data.Count });
        }


        // PUT: api/Orders/5
        [HttpPut]
        public object Put(int id, [FromBody]OrdersDetails value)
        {


            var ord = value;
            OrdersDetails val = OrdersDetails.GetAllRecords().Where(or => or.OrderID == ord.OrderID).FirstOrDefault();
            val.OrderID = ord.OrderID;
            val.EmployeeID = ord.EmployeeID;
            val.CustomerID = ord.CustomerID;
            val.Freight = ord.Freight;
            val.OrderDate = ord.OrderDate;
            val.ShipCity = ord.ShipCity;
            val.Info.FirstName = ord.Info.FirstName;
            val.Info.LastName = ord.Info.LastName;
            val.Info.Country = ord.Info.Country;
            return value;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id:int}")]
        [Route("Orders/{id:int}")]
        public object Delete(int id)
        {
            OrdersDetails.GetAllRecords().Remove(OrdersDetails.GetAllRecords().Where(or => or.OrderID == id).FirstOrDefault());
            return Json(id);
        }
    }
    public class OrdersDetails
    {
        public static List<OrdersDetails> order = new List<OrdersDetails>();
        public OrdersDetails()
        {

        }
        public OrdersDetails(int OrderID, string CustomerId, int EmployeeId, double Freight, bool Verified, DateTime OrderDate, string ShipCity, string ShipName, string ShipCountry, DateTime ShippedDate, string ShipAddress, CustomerDetails Info)
        {
            this.OrderID = OrderID;
            this.CustomerID = CustomerId;
            this.EmployeeID = EmployeeId;
            this.Freight = Freight;
            this.ShipCity = ShipCity;
            this.Verified = Verified;
            this.OrderDate = OrderDate;
            this.ShipName = ShipName;
            this.ShipCountry = ShipCountry;
            this.ShippedDate = ShippedDate;
            this.ShipAddress = ShipAddress;
            this.Info = Info;
        }
        public static List<OrdersDetails> GetAllRecords()
        {
            if (order.Count() == 0)
            {
                int code = 10000;
                for (int i = 1; i < 10; i++)
                {
                    order.Add(new OrdersDetails(code + 1, "ALFKI", i + 0, 2.3 * i, false, new DateTime(1991, 05, 15), "Berlin", "Simons bistro", "Denmark", new DateTime(1996, 7, 16), "Kirchgasse 6", new CustomerDetails(1, "Denmark", "Jancy", "Ram")));
                    order.Add(new OrdersDetails(code + 2, "ANATR", i + 2, 3.3 * i, true, new DateTime(1990, 04, 04), "Madrid", "Queen Cozinha", "Brazil", new DateTime(1996, 9, 11), "Avda. Azteca 123", new CustomerDetails(2, "Brazil", "John", "Victor")));
                    order.Add(new OrdersDetails(code + 3, "ANTON", i + 1, 4.3 * i, true, new DateTime(1957, 11, 30), "Cholchester", "Frankenversand", "Germany", new DateTime(1996, 10, 7), "Carrera 52 con Ave. Bolívar #65-98 Llano Largo", new CustomerDetails(3, "Germany", "Nick", "JR")));
                    order.Add(new OrdersDetails(code + 4, "BLONP", i + 3, 5.3 * i, false, new DateTime(1930, 10, 22), "Marseille", "Ernst Handel", "Austria", new DateTime(1996, 12, 30), "Magazinweg 7", new CustomerDetails(4, "Austria", "Mark", "Wood")));
                    order.Add(new OrdersDetails(code + 5, "BOLID", i + 4, 6.3 * i, true, new DateTime(1953, 02, 18), "Tsawassen", "Hanari Carnes", "Switzerland", new DateTime(1997, 12, 3), "1029 - 12th Ave. S.", new CustomerDetails(5, "Switzerland", "Angelina", "Sheebha")));
                    code += 5;
                }
            }
            return order;
        }

        public int? OrderID { get; set; }
        public string CustomerID { get; set; }
        public int? EmployeeID { get; set; }
        public double? Freight { get; set; }
        public string ShipCity { get; set; }
        public bool Verified { get; set; }
        public DateTime OrderDate { get; set; }

        public string ShipName { get; set; }

        public string ShipCountry { get; set; }

        public DateTime ShippedDate { get; set; }
        public string ShipAddress { get; set; }
        public CustomerDetails Info { get; set; }

    }
}
