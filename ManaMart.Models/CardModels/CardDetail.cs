using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManaMart.Models.CardModels
{
    public class CardDetail
    {
        public int CardId { get; set; }
        public string CardName { get; set; }
        public string ManaType { get; set; }
        public string ManaCost { get; set; }
        public string CardType { get; set; }

    }
}
