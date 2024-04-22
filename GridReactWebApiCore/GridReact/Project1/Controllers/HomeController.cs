using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Syncfusion.EJ2.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.Controllers
{
    public class HomeController : Controller
    {
        // GET: HomeController
        public ActionResult Index()
        {
            return View();
        }

        public IActionResult UrlDataSource([FromBody] DataManagerRequest dm)
        {
            IEnumerable<BigData> DataSource = BigData.GetAllRecords().AsEnumerable();
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
            int count = DataSource.Cast<BigData>().Count();
            if (dm.Skip != 0)
            {
                DataSource = operation.PerformSkip(DataSource, dm.Skip);   //Paging
            }
            if (dm.Take != 0)
            {
                DataSource = operation.PerformTake(DataSource, dm.Take);
            }
            return dm.RequiresCounts ? Json(new { result = DataSource, count = count }) : Json(DataSource);
        }
        public ActionResult Update([FromBody] CRUDModel<BigData> value)
        {
            var ord = value;

            BigData val = BigData.GetAllRecords().Where(or => or.OrderID == ord.Value.OrderID).FirstOrDefault();
            val.OrderID = ord.Value.OrderID;
            val.EmployeeID = ord.Value.EmployeeID;
            val.CustomerID = ord.Value.CustomerID;
            val.ShipCountry = ord.Value.ShipCountry;
            return Json(value);

        }
        public IActionResult Insert([FromBody] CRUDModel<BigData> value)
        {
            var Order = BigData.GetAllRecords();
            var obj = BigData.GetAllRecords().Where(or => or.OrderID.Equals(int.Parse(value.Value.OrderID.ToString()))).FirstOrDefault();
            BigData.GetAllRecords().Insert(0, value.Value);
            return Json(new { value.Value });
        }
        public IActionResult Delete([FromBody] CRUDModel<BigData> value)
        {
            if (value.Key == null)
            {
                List<BigData> records = new List<BigData> { };
                for (int i = 0; i < value.Deleted.Count; i++)
                {
                    BigData.GetAllRecords().Remove(BigData.GetAllRecords().Where(or => or.OrderID == int.Parse(value.Deleted[i].OrderID.ToString())).FirstOrDefault());
                }

                return Json(value);
            }
            else
            {
                BigData.GetAllRecords().Remove(BigData.GetAllRecords().Where(or => or.OrderID == int.Parse(value.Key.ToString())).FirstOrDefault());
                return Json(value);
            }
           
        }
        public class BigData
        {
            public static List<BigData> order = new List<BigData>();
            public BigData()
            {

            }
            public BigData(int OrderID, int EmployeeID, int N1, int N2, string CustomerID, int QuestionTypeId, double Freight, bool Verified, DateTime? OrderDate, string ShipCity, string ShipName, string ShipCountry, DateTime ShippedDate, string ShipAddress)
            {
                this.OrderID = OrderID;
                this.EmployeeID = EmployeeID;
                this.N1 = N1;
                this.N2 = N2;
                this.CustomerID = CustomerID;
                this.QuestionTypeId = QuestionTypeId;
                this.Freight = Freight;
                this.ShipCity = ShipCity;
                this.Verified = Verified;
                this.OrderDate = OrderDate;
                this.ShipName = ShipName;
                this.ShipCountry = ShipCountry;
                this.ShippedDate = ShippedDate;
                this.ShipAddress = ShipAddress;
            }
            public static List<BigData> GetAllRecords()
            {
                if (order.Count() == 0)
                {
                    int code = 10000;
                    for (int i = 1; i < 300; i++)
                    {
                        order.Add(new BigData(code + 1,1, 15, 10, "ALFKI", 4, 1112.3 * i, false, DateTime.Now, "#ff00ff", "Simons bistro", "Denmark", new DateTime(1996, 7, 16), "Kirchgasse 6"));
                        order.Add(new BigData(code + 2,2, 20, 8, "ANATR", 2, 456433.3 * i, true, new DateTime(1990, 04, 04), "#ffee00", "Queen Cozinha", "Brazil", new DateTime(1996, 9, 11), "Avda. Azteca 123"));
                        order.Add(new BigData(code + 3,3, 22, 15, "ANTON", 1, 6544.3 * i, true, new DateTime(1957, 11, 30), "#110011", "Frankenversand", "Germany", new DateTime(1996, 10, 7), "Carrera 52 con Ave. Bolívar #65-98 Llano Largo"));
                        order.Add(new BigData(code + 4,4, 18, 11, "BLONP", 3, 455.3 * i, false, new DateTime(1930, 10, 22), "#ff5500", "Ernst Handel", "Austria", new DateTime(1996, 12, 30), "Magazinweg 7"));
                        order.Add(new BigData(code + 5, 5,26, 13, "BOLID", 4, 63.3 * i, true, new DateTime(1953, 02, 18), "#aa0088", "Hanari Carnes", "Switzerland", new DateTime(1997, 12, 3), "1029 - 12th Ave. S."));
                        code += 5;
                    }
                }
                return order;
            }
            public int? OrderID { get; set; }
            public int? N1 { get; set; }
            public int? N2 { get; set; }
            public int? EmployeeID { get; set; }
            public string CustomerID { get; set; }
            public int? QuestionTypeId { get; set; }
            public double? Freight { get; set; }
            public string ShipCity { get; set; }
            public bool Verified { get; set; }
            public DateTime? OrderDate { get; set; }
            public string ShipName { get; set; }
            public string ShipCountry { get; set; }
            public DateTime ShippedDate { get; set; }
            public string ShipAddress { get; set; }
        }
    }
}
