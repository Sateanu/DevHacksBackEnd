using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevHacksServer.Models
{
    public class Cluster
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Clusters ToEntity()
        {
            return new Clusters()
            {
                Id=Id,
                Latitude=Latitude,
                Longitude=Longitude
            };

        }
    }
}
