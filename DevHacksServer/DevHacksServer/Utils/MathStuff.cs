using DevHacksServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevHacksServer.Utils
{
    public class MathStuff
    {
        internal const double EarthsRadiusInKilometers=6371;

        public static double FromCoordsToMeters(double fromLatitude, double fromLongitude, double toLatitude, double toLongitude)
        {
            return (Math.Acos(
                Math.Sin(fromLatitude) * Math.Sin(toLatitude) +
                Math.Cos(fromLatitude) * Math.Cos(toLatitude) *
                Math.Cos(toLongitude - fromLongitude)
            ) * EarthsRadiusInKilometers)
            .ToRadians();
        }

    }
}
