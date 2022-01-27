using ManaMart.Data;
using ManaMart.Models.CardModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManaMart.Services
{
    public class CardService
    {
        ApplicationDbContext _ctx = new ApplicationDbContext();

        private readonly Guid _userId;
        public CardService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateCard(CardCreate model)
        {
            var entity =
                new Card()
                {
                    OwnerId = _userId,
                    CardName = model.CardName,
                    ManaType = model.ManaType,
                    ManaCost = model.ManaCost,
                    CardType = model.CardType

                };
            _ctx.Cards.Add(entity);
            return _ctx.SaveChanges() == 1;
        }

        public IEnumerable<CardListItem> GetCards()
        {
            var query =
                _ctx
                .Cards
                .Where(e => e.OwnerId == _userId)
                .Select(
                    e =>
                    new CardListItem
                    {
                        CardId = e.CardId,
                        CardName = e.CardName,
                        CardType = e.CardType
                    }
                    );
            return query.ToArray();
        }

        public CardDetail GetCardById(int id)
        {
            var entity =
                _ctx
                .Cards
                .Single(e => e.CardId == id && e.OwnerId == _userId);
            CardDetail detail =
                new CardDetail
                {
                    CardId = entity.CardId,
                    CardName = entity.CardName,
                    CardType = entity.CardType,
                    ManaCost = entity.ManaCost,
                    ManaType = entity.ManaType
                };
            return detail;
        }

        public CardEdit EditCardById(int id)
        {
            var entity =
                _ctx
                .Cards
                .Single(e => e.CardId == id && e.OwnerId == _userId);
            return new CardEdit
            {
                CardId = entity.CardId,
                CardName = entity.CardName,
                CardType = entity.CardType,
                ManaCost = entity.ManaCost,
                ManaType = entity.ManaType
            };
        }

        public bool UpdateCard(CardEdit model)
        {
            var entity =
                _ctx
                .Cards
                .Single(e => e.CardId == model.CardId && e.OwnerId == _userId);

            entity.CardId = model.CardId;
            entity.CardName = model.CardName;
            entity.CardType = model.CardType;
            entity.ManaType = model.ManaType;
            entity.ManaCost = model.ManaCost;

            return _ctx.SaveChanges() == 1;
        }

        public bool DeleteCard(int cardId)
        {
            var entity =
                _ctx
                    .Cards
                    .Single(e => e.CardId == cardId && e.OwnerId == _userId);
            _ctx.Cards.Remove(entity);

            return _ctx.SaveChanges() == 1;
        }
    }


}
