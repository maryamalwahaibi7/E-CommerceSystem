using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace E_CommerceSystem.Models
{
    public class Review
    {
        [Key]
        public int R_Id { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        public string? Comment { get; set; }

        public DateTime? ReviewDate { get; set; }

        [ForeignKey("User")]
        public int U_Id { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("Product")]
        public int P_Id { get; set; }
        public virtual Product Product { get; set; }


    }
}
