using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManaMart.Data
{
    public class Deck
    {
        [Key]
        public int DeckId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string DeckName { get; set; }

        [Required]
        [Display(Name = "Deck Type")]  //Aggro //Flash //Dino etc
        public string DeckType { get; set; }

        public Guid OwnerId { get; set; }
    }
}
