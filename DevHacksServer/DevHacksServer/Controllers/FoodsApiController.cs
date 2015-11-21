using DevHacksServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DevHacksServer.Controllers
{
    public class FoodsApiController : ApiController
    {
        Entities db = new Entities();
       
        public IQueryable<Food> GetFoods()
        {
            return db.Foods.Select(food=> new Food()
            {
                Id = food.Id,
                Image = food.Image,
                Category = food.Category,
                Name = food.Name,
                RestaurantID = food.RestaurantID,
                Description = food.Description,
                Price = food.Price
            }); 
        }

        /// <summary>
        /// Take the food from the restaurant
        /// </summary>
        /// <param name="idR">The id of the restaurant</param>
        /// <returns></returns>
        public IQueryable<Food> GetFoods(int id)
        {
            var foods = db.Foods.Where(x => x.RestaurantID == id);
            return foods.Select(food => new Food()
            {
                Id = food.Id,
                Image = food.Image,
                Category = food.Category,
                Name = food.Name,
                RestaurantID = food.RestaurantID,
                Description = food.Description,
                Price = food.Price
            });
        }

        public Food GetFood(int id)
        {
            return db.Foods.Find(id).toModel();
        }


    }
}
