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
    using System.Collections.Generic;
    
    public partial class Clusters
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Clusters()
        {
            this.Orders = new HashSet<Orders>();
        }
    
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int RestaurantID { get; set; }
        public long Time { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual Restaurants Restaurants { get; set; }
    }
}
