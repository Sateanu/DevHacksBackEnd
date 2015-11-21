using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevHacksServer.Models
{
    public class Suborder
    {
        public int Id { get; set; }
        public int FoodID { get; set; }
        public int OrderID { get; set; }
        public int Quantity { get; set; }

        internal Suborders ToEntity()
        {
            return new Suborders()
            {
                Id=Id,
                FoodID=FoodID,
                OrderID=OrderID,
                Quantity=Quantity
            };
        }
    }
}
