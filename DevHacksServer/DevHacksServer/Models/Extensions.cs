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
                Name=rest.Name
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
            newOrder.Suborders = new List<Suborder>();
            foreach (var sub in o.Suborders)
            {
                newOrder.Suborders.Add(sub.ToModel());
            }

            return newOrder;
            
        }

        public static double ToRadians(this double d)
        {
            return (Math.PI / 180) * d;
        }

    }
}