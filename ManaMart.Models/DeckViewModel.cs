using ManaMart.Models.DeckModels;
using ManaMart.Models.JoinModels.DeckCardModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManaMart.Models
{
    public class DeckViewModel
    {
        public List<DeckCardDetail> Cards { get; set; } = new List<DeckCardDetail>();

        public List<DeckDetail> Decks { get; set; } = new List<DeckDetail>();

       public DeckEdit DeckDetail { get; set; }

    }
}
