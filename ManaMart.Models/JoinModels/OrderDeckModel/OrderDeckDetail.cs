using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManaMart.Models.JoinModels.OrderDeckModel
{
    public class OrderDeckDetail
    {
        public int OrderDeckId { get; set; }
        public int OrderId { get; set; }
        public int DeckId { get; set; }

        [Display(Name = "Name")]
        public string DeckName { get; set; }

        [Display(Name = "Deck Type")]  //Commander //Standard
        public string DeckType { get; set; }
    }
}
