using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DevHacksServer.Models;

namespace DevHacksServer.Controllers
{
    public class RestaurantsApiController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/RestaurantsApi
        public IQueryable<Restaurant> GetRestaurants()
        {
            return db.Restaurants.Select(x => new Restaurant()
            {
                Id = x.Id,
                Name = x.Name,
                Latitude = x.Latitude,
                Longitude=x.Longitude,
                Location=x.Location,
                Specific=x.Specific
            });
        }

        // GET: api/RestaurantsApi/5
        [ResponseType(typeof(Restaurants))]
        public IHttpActionResult GetRestaurants(int id)
        {
            Restaurants restaurants = db.Restaurants.Find(id);
            if (restaurants == null)
            {
                return NotFound();
            }
            return Ok(restaurants.toModel());
        }

        // PUT: api/RestaurantsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRestaurants(int id, Restaurants restaurants)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != restaurants.Id)
            {
                return BadRequest();
            }

            db.Entry(restaurants).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/RestaurantsApi
        [ResponseType(typeof(Restaurants))]
        public IHttpActionResult PostRestaurants(Restaurants restaurants)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Restaurants.Add(restaurants);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = restaurants.Id }, restaurants);
        }

        // DELETE: api/RestaurantsApi/5
        [ResponseType(typeof(Restaurants))]
        public IHttpActionResult DeleteRestaurants(int id)
        {
            Restaurants restaurants = db.Restaurants.Find(id);
            if (restaurants == null)
            {
                return NotFound();
            }

            db.Restaurants.Remove(restaurants);
            db.SaveChanges();

            return Ok(restaurants);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RestaurantsExists(int id)
        {
            return db.Restaurants.Count(e => e.Id == id) > 0;
        }
    }
}