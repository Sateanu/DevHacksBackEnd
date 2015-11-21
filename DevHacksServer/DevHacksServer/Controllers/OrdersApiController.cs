using DevHacksServer.Models;
using DevHacksServer.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DevHacksServer.Controllers
{
    public class OrdersApiController : ApiController
    {
        Entities db = new Entities();

        public IEnumerable<Order> GetOrders()
        {
            return db.Orders.ToList().Select(x => x.ToModel());
        }
        [HttpPost]
        public void PutOrder(Order order)
        {

        }
    }
}
