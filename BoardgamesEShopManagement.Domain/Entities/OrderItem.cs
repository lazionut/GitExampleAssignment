﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement.Domain.Entities
{
    public class OrderItem
    { 
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public int BoardgameId { get; set; }
        public Boardgame Boardgame { get; set; } = null!;
    }
}
