﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBank.API.Models
{
    public class Deposit
    {
        [Key]
        public Guid DepositId { get; set; }
        [Required]
        public Guid AccountId { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
