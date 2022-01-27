using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManaMart.Models.OrderModels
{
    public class OrderCreate
    {
        [Required]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        public string OrderDate { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
