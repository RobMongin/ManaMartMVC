﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManaMart.Models.JoinModels.OrderDeckModel
{
    public class OrderDeckEdit
    {
        public int OrderDeckId { get; set; }
        public int OrderId { get; set; }
        public int DeckId { get; set; }
    }
}
