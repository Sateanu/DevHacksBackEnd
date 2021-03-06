﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevHacksServer.Models
{
    public static class Extensions
    {
        public static Restaurant ToModel(this Restaurants rest)
        {
            return new Restaurant()
            {
                Id=rest.Id,
                Latitude=rest.Latitude,
                Longitude=rest.Longitude,
                Location=rest.Location,
                Specific=rest.Specific,
                Name=rest.Name,
                Email=rest.Email,
                Url=rest.Url
            };
        }

        public static Food ToModel(this Foods food)
        {
            return new Food()
            {
                Id = food.Id,
                Image = food.Image,
                Category = food.Category,
                Name = food.Name,
                RestaurantID = food.RestaurantID,
                Description = food.Description,
                Price = food.Price
            };

        }

        public static Suborder ToModel(this Suborders sb)
        {
            return new Suborder()
            {
                Id = sb.Id,
                FoodID = sb.FoodID,
                OrderID = sb.OrderID,
                Quantity = sb.Quantity
            };
        }

        public static Order ToModel(this Orders o)
        {
            Order newOrder = new Order();

            newOrder.Id = o.Id;
            newOrder.Price = o.Price;
            newOrder.RestaurantID = o.RestaurantID;
            newOrder.Discount = o.Discount;
            newOrder.Time = o.Time;
            newOrder.Latitude = o.Latitude;
            newOrder.Longitude = o.Longitude;
            newOrder.ClusterID = o.ClusterID;
            newOrder.UserID = o.UserID;
            newOrder.Done = o.Done;
            newOrder.Location = o.Location;
            newOrder.Suborders = new List<Suborder>();
            foreach (var sub in o.Suborders)
            {
                newOrder.Suborders.Add(sub.ToModel());
            }

            return newOrder;
        }

        public static Order ToModel(this GetOrdersInArea_Result o)
        {
            Order newOrder = new Order();

            newOrder.Id = o.Id;
            newOrder.Price = o.Price;
            newOrder.RestaurantID = o.RestaurantID;
            newOrder.Discount = o.Discount;
            newOrder.Time = o.Time;
            newOrder.Latitude = o.Latitude;
            newOrder.Longitude = o.Longitude;
            newOrder.ClusterID = o.ClusterID;
            newOrder.UserID = o.UserID;
            newOrder.Done = o.Done;
            newOrder.Location = o.Location;
            newOrder.Suborders = new List<Suborder>();
            Entities db = new Entities();
            var suborders = db.Suborders.Where(x => x.OrderID == o.Id);
            foreach (var sub in suborders)
            {
                newOrder.Suborders.Add(sub.ToModel());
            }

            return newOrder;
        }

        public static Cluster ToModel(this Clusters cl)
        {
            Cluster cluster = new Cluster();
            cluster.Id = cl.Id;
            cluster.Latitude = cl.Latitude;
            cluster.Longitude = cl.Longitude;
            cluster.RestaurantID = cl.RestaurantID;
            cluster.Time = cl.Time;
            return cluster;
        }

        public static Cluster ToModel(this GetClustersInArea_Result cl)
        {
            Cluster cluster = new Cluster();
            cluster.Id = cl.Id;
            cluster.Latitude = cl.Latitude;
            cluster.Longitude = cl.Longitude;
            cluster.Time = cl.Time;
            return cluster;
        }

        public static double ToRadians(this double d)
        {
            return (Math.PI / 180) * d;
        }

        private static readonly DateTime Jan1St1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        /// <summary>Get extra long current timestamp</summary>
        public static long Millis { get { return (long)((DateTime.UtcNow - Jan1St1970).TotalMilliseconds); } }
    }
}
