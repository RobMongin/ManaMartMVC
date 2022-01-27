using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManaMart.Models.JoinModels.DeckCardModel
{
    public class DeckCardEdit
    {
        public int DeckCardId { get; set; }
        public int DeckId { get; set; }
        public int CardId { get; set; }
        public int Quantity { get; set; }


    }
}
