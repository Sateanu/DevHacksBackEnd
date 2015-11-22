using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevHacksServer.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int RestaurantID { get; set; }
        public double Price { get; set; }
        public int Discount { get; set; }
        public long Time { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int UserID { get; set; }
        public int Done { get; set; }
        public Nullable<int> ClusterID { get; set; }
        public List<Suborder> Suborders { get; set; }
        public string Location { get; set; }

        internal Orders ToEntity()
        {
            Orders order = new Orders();
            order.Discount = Discount;
            order.Id = Id;
            order.RestaurantID = RestaurantID;
            order.Price = Price;
            order.Time = Time;
            order.Longitude = Longitude;
            order.Latitude = Latitude;
            order.UserID = UserID;
            order.Done = Done;
            order.ClusterID = ClusterID;
            order.Location = Location;
            foreach (var so in Suborders)
            {
                order.Suborders.Add(so.ToEntity());
            }
            return order;
        }
    }
}
