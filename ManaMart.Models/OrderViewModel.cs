using ManaMart.Models.JoinModels.OrderDeckModel;
using ManaMart.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManaMart.Models
{
    public class OrderViewModel
    {
        public List<OrderDeckDetail> Decks { get; set; } = new List<OrderDeckDetail>();

        public List<OrderDetail> Orders { get; set; } = new List<OrderDetail>();

        public OrderEdit OrderDetail { get; set; }
    }
}
