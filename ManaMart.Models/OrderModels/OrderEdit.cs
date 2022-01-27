using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManaMart.Models.OrderModels
{
    public class OrderEdit
    {
        public int OrderId
        {
            get; set;
        }
        public string OrderDate
        {
            get; set;
        }
        public string PhoneNumber { get; set; }
        public string CustomerName { get; set; }
    }
}
