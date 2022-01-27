using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManaMart.Models.CardModels
{
    public class CardCreate
    {
        [Required]
        [Display(Name = "Name")]
        public string CardName { get; set; }

        [Required]
        [Display(Name = "Mana Type")]
        public string ManaType { get; set; }

        [Required]
        [Display(Name = "Mana Cost")]
        public string ManaCost { get; set; }

        [Required]
        [Display(Name = "Card Type")] //enchant, instant, sorcery, creature, plainswalker
        public string CardType { get; set; }
    }
}
