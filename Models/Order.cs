using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace E_CommerceSystem.Models
{
    public class Order
    {
        [Key]
        public int O_Id { get; set; }

        public DateTime? OrderDate { get; set; }

        public decimal? TotalAmount { get; set; }

        [ForeignKey("User")]
        public int U_Id { get; set; }
        public virtual User User { get; set; } 

        public virtual ICollection<OrderProduct> OrderProducts { get; set; } 
    }
}
