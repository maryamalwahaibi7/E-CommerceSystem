using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace E_CommerceSystem.Models
{
    public class Product
    {
        [Key]
        public int P_Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Stock { get; set; } 

        public decimal? OverallRating { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
