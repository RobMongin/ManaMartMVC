using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManaMart.Data
{
    public class OrderDeck
    {
        [Key]
        public int OrderDeckId { get; set; }

        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        [ForeignKey(nameof(Deck))]
        public int DeckId { get; set; }
        public virtual Deck Deck { get; set; }

        public Guid OwnerId { get; set; }
    }
}
