using System.Collections;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.EJ2.Base;
using Microsoft.AspNetCore.SignalR;

namespace CoreGridApp.Controllers
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

    public IActionResult RemoteSaveDataSource([FromBody] DataManagerRequest dm)
    {
      
      return Json(order);
    }

    public async Task<IActionResult> SendGridUpdateNotification([FromServices] IHubContext<GridUpdateHub> hubContext)
    {
      await hubContext.Clients.All.SendAsync("ReceiveGridUpdate");
      return Ok();
    }

    public IActionResult Insert([FromBody] CRUDModel<Orders> value, [FromServices] IHubContext<GridUpdateHub> hubContext)
    {
      order.Insert(0, value.Value);

      // send notification to connected clients through SignalR
      // hubContext.Clients.All.SendAsync("ReceiveGridUpdate");

      return Json(value.Value);
    }
    public ActionResult getStatus([FromBody] Orders value)
    {
      if (value.EmployeeID <= 5)
      {
        return new JsonResult(new { result = false, customText = "The value cannot be lower than 5" });
      }
      else if (value.EmployeeID >= 20)
      {
        return new JsonResult(new { result = false, customText = "The value cannot be  higher that 20" });
      }
      else
      {
        return new JsonResult(new { result = true });
      }
    }

    public IActionResult Update([FromBody] CRUDModel<Orders> value, [FromServices] IHubContext<GridUpdateHub> hubContext)
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
      
      // send notification to connected clients through SignalR
      // hubContext.Clients.All.SendAsync("ReceiveGridUpdate");

      return Json(value.Value);
    }
    public IActionResult Delete([FromBody] CRUDModel<Orders> Value, [FromServices] IHubContext<GridUpdateHub> hubContext)
    {
      order.Remove(order.First(or => or.OrderID == ((JsonElement)Value.Key).GetInt64()));
      
      // send notification to connected clients through SignalR
      // hubContext.Clients.All.SendAsync("ReceiveGridUpdate");
      
      return Json(Value);
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
   public class OrdersLower
    {
      public OrdersLower(long orderId, string customerId, int employeeId, double freight, DateTime? orderDate, string shipCity)
      {
        this.orderID = orderId;
        this.customerID = customerId;
        this.employeeID = employeeId;
        this.freight = freight;
        this.orderDate = orderDate;
        this.shipCity = shipCity;
      }
      public long orderID { get; set; }
      public string customerID { get; set; }
      public int employeeID { get; set; }
      public double freight { get; set; }
      public DateTime? orderDate { get; set; }
      public string shipCity { get; set; }
    }

    public IActionResult Error()
    {
      ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
      return View();
    }
  }
}
