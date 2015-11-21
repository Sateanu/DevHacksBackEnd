using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevHacksServer.Models
{
    public static class Extensions
    {
        public static Restaurant toModel(this Restaurants rest)
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
    }
}