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
        public IEnumerable<Order> GetOrderUser([FromBody]string email)
        {
            var user = db.Users.Where(x => x.Email == email).FirstOrDefault();
            if (user == null)
                return null;

            var orders = db.Orders.Where(x => x.UserID == user.Id).ToList().Select(x=>x.ToModel());
            return orders;

        }

        public void PostOrder(Order order)
        {
            var clusters = db.GetClustersInArea(order.Latitude, order.Longitude, Constants.MaxRadius);
            if (clusters.Count() > 0)
            {
                foreach (var cluster in clusters)
                {
                    
                }
            }
            else
            {
                var a = db.Clusters.Add(new Clusters() {Latitude=order.Latitude,Longitude=order.Longitude });
                db.SaveChanges();
                order.ClusterID = a.Id;
                db.Orders.Add(order.ToEntity());
                db.SaveChanges();
            }

            //db.Orders.Add(order.ToEntity());
            //db.SaveChanges();
        }
    }
}
