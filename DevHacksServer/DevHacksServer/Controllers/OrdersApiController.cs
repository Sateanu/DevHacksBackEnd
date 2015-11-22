using DevHacksServer.Models;
using DevHacksServer.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
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

        public IEnumerable<Order> GetGoodOrders()
        {
            var date = DateTime.Now.Ticks;
            return db.Orders.Where(x=>x.Done==0&&x.Time<=date).ToList().Select(x => x.ToModel());
        }

        public void SetOrderDone(int id)
        {
            var order = db.Orders.Where(x => x.Id == id).FirstOrDefault();
            if(order!=null)
            {
                order.Done = 1;
                db.Entry(order).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void GetMagic()
        {
            var orders = GetGoodOrders();
            foreach (var order in orders)
            {
                KronkPullTheLever(order);
            }
        }

        private void KronkPullTheLever(Order order)
        {
            //SendTheOwl("andrei.sateanu@gmail.com");
            SendTheOwl("alexbuicescu@gmail.com");
        }

        public bool SendTheOwl(string email)
        {
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress("andrei.sateanu@gmail.com");
            msg.To.Add(email);
            msg.Subject = "BRRRRRRRRRRRRRRRAAAAAAAAAAAAAAAAAAAAAAAAA " + DateTime.Now.ToString();
            msg.Body = "MERGE BAAAA";
            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = true;
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new NetworkCredential("andrei.sateanu@gmail.com", "zewycawzedktrfvj");
            client.Timeout = 20000;
            try
            {
                client.Send(msg);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                msg.Dispose();
            }
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
            var clusters = db.GetClustersInAreaEntity(order.Latitude, order.Longitude, Constants.MaxRadius).Where(x=>x.RestaurantID==order.RestaurantID&&x.Time==order.Time).ToList();
            if (clusters!=null&&clusters.Count() > 0)
            {
                int clMinId = -1;
                double distMin = 99999;
                double clNewLat=0;
                double clNewLong = 0;
                foreach (var cluster in clusters)
                {
                    var clusterOrders = db.Orders.Where(x => x.ClusterID == cluster.Id);
                    double latitudeG= clusterOrders.Sum(x=>x.Latitude);
                    double longitudeG= clusterOrders.Sum(x => x.Longitude);
                    var n = cluster.Orders.Count;
                    latitudeG += order.Latitude;
                    longitudeG += order.Longitude;
                    latitudeG /= n+1;
                    longitudeG /= n+1;
                    var theOrders = db.GetOrdersInArea(latitudeG, longitudeG, Constants.MaxRadius).ToList();
                    var clusterOrdersIds = clusterOrders.Select(x => x.Id).ToList();
                    var theOrdersIds = theOrders.Select(x => x.Id).ToList();
                    if(clusterOrdersIds.Any(x=>theOrdersIds.Contains(x)))
                    {
                        double dist = theOrders.Min(x => x.Distance).GetValueOrDefault(0);
                        if (dist<distMin)
                        {
                            distMin = dist;
                            clMinId = cluster.Id;
                            clNewLat = latitudeG;
                            clNewLong = longitudeG;
                        }
                    }
                }
                if(clMinId!=-1)
                {
                    order.ClusterID = clMinId;
                    var cl = db.Clusters.Find(clMinId);
                    cl.Latitude = clNewLat;
                    cl.Longitude = clNewLong;
                    db.Entry(cl).State = System.Data.Entity.EntityState.Modified;
                    db.Orders.Add(order.ToEntity());
                    db.SaveChanges();
                }
                else
                {
                    var a = db.Clusters.Add(new Clusters() { Latitude = order.Latitude, Longitude = order.Longitude,RestaurantID=order.RestaurantID });
                    db.SaveChanges();
                    order.ClusterID = a.Id;
                    db.Orders.Add(order.ToEntity());
                    db.SaveChanges();
                }
            }
            else
            {
                var a = db.Clusters.Add(new Clusters() {Latitude=order.Latitude,Longitude=order.Longitude,RestaurantID=order.RestaurantID });
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
