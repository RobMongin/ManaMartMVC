using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManaMart.Data
{
    public class DeckCard
    {
        [Key]
        public int DeckCardId { get; set; }

        [ForeignKey(nameof(Card))]
        public int CardId { get; set; }
        public virtual Card Card { get; set; }


        [ForeignKey(nameof(Deck))]
        public int DeckId { get; set; }
        public virtual Deck Deck { get; set; }

        [Required]
        public int Quantity { get; set; }

        public Guid OwnerId { get; set; }

    }
    
}
