using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.EJ2.Base;

namespace ReactCrudDemo.Controllers
{
    public class HomeController : Controller
    {
        public static List<Orders> order = new List<Orders>();
        public IActionResult Index()
        {
            if (order.Count == 0)
                BindDataSource();
            ViewBag.datasource = order;
            return View();
        }
        public IActionResult UrlDatasource([FromBody]DataManagerRequest dm)
        {
            var gridData = order;
            IEnumerable DataSource = gridData;
            DataOperations operation = new DataOperations();
            if (dm.Search != null && dm.Search.Count > 0)
            {
                DataSource = operation.PerformSearching(DataSource, dm.Search);  //Search
            }
            if (dm.Sorted != null && dm.Sorted.Count > 0) //Sorting
            {
                DataSource = operation.PerformSorting(DataSource, dm.Sorted);
            }
            if (dm.Where != null && dm.Where.Count > 0) //Filtering
            {
                DataSource = operation.PerformFiltering(DataSource, dm.Where, dm.Where[0].Operator);
            }
            int count = DataSource.Cast<Orders>().Count();
            if (dm.Skip != 0)
            {
                DataSource = operation.PerformSkip(DataSource, dm.Skip);         //Paging
            }
            if (dm.Take != 0)
            {
                DataSource = operation.PerformTake(DataSource, dm.Take);
            }
            return dm.RequiresCounts ? Json(new { result = DataSource, count = count }) : Json(DataSource);
        }

        public IActionResult Insert([FromBody]CRUDModel<Orders> value)
        {
            order.Insert(0, value.Value);
            return Json(value.Value);
        }
        public IActionResult Update([FromBody]CRUDModel<Orders> value)
        {
            var data = order.Where(or => or.OrderID == value.Value.OrderID).FirstOrDefault();
            if (data != null)
            {
                data.OrderID = value.Value.OrderID;
                data.CustomerID = value.Value.CustomerID;
                data.EmployeeID = value.Value.EmployeeID;
                data.OrderDate = value.Value.OrderDate;
                data.ShipCity = value.Value.ShipCity;
                data.Freight = value.Value.Freight;
            }
            return Json(value.Value);
        }
        public void Remove([FromBody]CRUDModel<Orders> Value)
        {
            var data = order.Where(or => or.OrderID.Equals(Value.Key)).FirstOrDefault();
            order.Remove(data);
        }        
        private void BindDataSource()
        {
            int code = 10000;
            for (int i = 1; i < 100; i++)
            {
                order.Add(new Orders(code + 1, "LàFKI", i + 0, 2.3 * i, new DateTime(1991, 05, 15), "Berlin"));
                order.Add(new Orders(code + 2, "ANATR", i + 2, 3.3 * i, new DateTime(2017, 08, 11), "Madrid"));
                order.Add(new Orders(code + 3, "ANTON", i + 1, 4.3 * i, new DateTime(1957, 11, 30), "Cholchester"));
                order.Add(new Orders(code + 4, "BLONP", i + 3, 5.3 * i, new DateTime(2019, 11, 11), "Marseille"));
                order.Add(new Orders(code + 5, "BOLID", i + 4, 6.3 * i, new DateTime(1953, 02, 18), "Tsawassen"));
                code += 5;
            }
        }
        public class Orders
        {
            public Orders()
            {

            }
            public Orders(long OrderId, string CustomerId, int EmployeeId, double Freight, DateTime? OrderDate, string ShipCity)
            {
                this.OrderID = OrderId;
                this.CustomerID = CustomerId;
                this.EmployeeID = EmployeeId;
                this.Freight = Freight;
                this.OrderDate = OrderDate;
                this.ShipCity = ShipCity;
            }
            public long OrderID { get; set; }
            public string CustomerID { get; set; }
            public int EmployeeID { get; set; }
            public double Freight { get; set; }
            public DateTime? OrderDate { get; set; }
            public string ShipCity { get; set; }
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
