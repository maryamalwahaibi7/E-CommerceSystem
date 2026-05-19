using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace E_CommerceSystem.Models
{
    [Index(nameof(Email), IsUnique = true)] 
    public class User
    {
        [Key]
        public int U_Id { get; set; }

        [Required]
        public string Name { get; set; }


        [EmailAddress]
        public string? Email { get; set; }

        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d).{8,}$")]
        public string? Password { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Order> Orders { get; set; } 


        public virtual ICollection<Review> Reviews { get; set; }







    }
}
