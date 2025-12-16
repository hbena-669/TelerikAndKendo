using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TelerikAndKendo.Models;

namespace TelerikAndKendo.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public List<Customer> CustomersData()
        {
            var result = Enumerable.Range(0, 100).Select(i => new Customer
            {
                OrderID = i,
                FirstName = "FirstName" + i,
                LastName = "LastaName" + i,
                City = "LastaName" + i,
                ShipName = "ShipName" + i,
                ShipCity = "ShipCity" + i,

            });
            return result.ToList();
        }
        public JsonResult GetCustomers()
        {
            return Json(CustomersData(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetShipNameList()
        {
            int pageSize = Convert.ToInt32(Request.Params.Get("pageSize"));
            int skip = Convert.ToInt32(Request.Params.Get("skip"));
            string search = Request.Params.Get("filter[filters][0][value]");
            var dataEnteredList = CustomersData();
            var total = dataEnteredList.Count();
            var data = dataEnteredList.Skip(skip).Take(pageSize).ToList();

            return Json(new { total = total, data = data }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ValueMapper(int?[] values)
        {
            var indices = new List<int>();

            if (values != null && values.Any())
            {
                var index = 0;
            }

            return Json(indices, JsonRequestBehavior.AllowGet);
        }
    }
}
