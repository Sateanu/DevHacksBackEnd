//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DevHacksServer.Models
{
    using System;
    
    public partial class GetClustersInArea_Result
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int RestaurantID { get; set; }
        public long Time { get; set; }
        public Nullable<double> Distance { get; set; }
    }
}
