using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace E_CommerceSystem.Models
{
    [PrimaryKey(nameof(O_Id), nameof(P_Id))]
    public class OrderProduct
    {
        [Required]
        [Range(1, int.MaxValue)] 
        public int Quantity { get; set; }

        [ForeignKey("Order")]
        public int O_Id { get; set; }
        public virtual Order Order { get; set; }

        [ForeignKey("Product")]
        public int P_Id { get; set; }
        public virtual Product Product { get; set; }
    }
}
