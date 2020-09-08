using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantManagement.Models
{
    public class Orders
    {
        public string Email { get; set; }
        [ForeignKey ("Food")]
        public int FoodId { get; set; }
        [Key]
        public int OrderId { get; set; }



    }
}
