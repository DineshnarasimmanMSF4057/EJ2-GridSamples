using System;
using System.Collections;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Text.Json;
using System.Web.Http;
using System.Web.Mvc;
using Syncfusion.EJ2.Base;

namespace mVC.Controllers
{
    public class HomeController : Controller
    {
        public static List<Orders> order = new List<Orders>();
        public ActionResult Index()
        {
            if (order.Count == 0)
                BindDataSource();
            ViewBag.datasource = order;
            return View();
        }
        public ActionResult UrlDatasource([FromBody] DataManagerRequest dm)
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
                DataSource = operation.PerformSkip(DataSource, dm.Skip); //Paging
            }
            if (dm.Take != 0)
            {
                DataSource = operation.PerformTake(DataSource, dm.Take);
            }
            return dm.RequiresCounts ? Json(new { result = DataSource, count }) : Json(DataSource);
        }


        public ActionResult BatchUpdate([FromBody] CRUDModel batchmodel)
        {
            if (batchmodel.Changed != null)
            {
                for (var i = 0; i < batchmodel.Changed.Count(); i++)
                {
                    Orders ord = System.Text.Json.JsonSerializer.Deserialize<Orders>((dynamic)batchmodel.Changed[i]);
                    Orders val = order.Where(or => or.OrderID == ord.OrderID).FirstOrDefault();
                    val.OrderID = ord.OrderID;
                    val.EmployeeID = ord.EmployeeID;
                    val.CustomerID = ord.CustomerID;
                    val.ShipCity = ord.ShipCity;
                }
            }
            if (batchmodel.Deleted != null)
            {
                for (var i = 0; i < batchmodel.Deleted.Count(); i++)
                {
                    order.Remove(order.Where(or => or.OrderID == ((dynamic)batchmodel.Deleted[i]).OrderID).FirstOrDefault());
                }
            }
            if (batchmodel.Added != null)
            {
                for (var i = 0; i < batchmodel.Added.Count(); i++)
                {
                    order.Insert(0, (Orders)batchmodel.Added[i]);
                }
            }
            var data = order.ToList();
            return Json(data);
        }


        public ActionResult Insert([FromBody] Orders value)
        {
            order.Insert(0, value);
            return Json(value);
        }
        public ActionResult Update([FromBody] Orders value)
        {
            var data = order.Where(or => or.OrderID == value.OrderID).FirstOrDefault();
            if (data != null)
            {
                data.OrderID = value.OrderID;
                data.CustomerID = value.CustomerID;
                data.EmployeeID = value.EmployeeID;
                data.OrderDate = value.OrderDate;
                data.ShipCity = value.ShipCity;
                data.Freight = value.Freight;
            }
            return Json(value);
        }
        public void Remove([FromBody] CRUDModel<Orders> Value)
        {
            order.Remove(order.Where(or => or.OrderID == int.Parse(Value.Key.ToString())).FirstOrDefault());
        }
        private static void BindDataSource()
        {
            int code = 10000;
            for (int i = 1; i <= 200; i++)
            {
                order.Add(new Orders(code + 1, "LOFKI", i + 0, 2.3 * i, new DateTime(1991, 05, 15), "Berlin"));
                order.Add(new Orders(code + 2, "ANATR", i + 2, 3.3 * i, new DateTime(2017, 08, 11), "Madrid"));
                order.Add(new Orders(code + 3, "ANTON", i + 1, 4.3 * i, new DateTime(1957, 11, 30), "Cholchester"));
                order.Add(new Orders(code + 4, "BLONP", i + 3, 5.3 * i, new DateTime(2019, 11, 11), "Marseille"));
                order.Add(new Orders(code + 5, "BOLID", i + 4, 6.3 * i, new DateTime(1953, 02, 18), "Tsawassen"));
                code += 5;
            }
        }
        public class Orders
        {
            public Orders() { }
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

        public ActionResult Error()
        {
            ViewData["RequestId"] = "00001";
            return View();
        }
    }
}
