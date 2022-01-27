using ManaMart.Data;
using ManaMart.Models.JoinModels.DeckCardModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManaMart.Services
{
    public class DeckCardService
    {
        ApplicationDbContext _ctx = new ApplicationDbContext();

        private readonly Guid _userId;
        public DeckCardService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateDeckCard(DeckCardCreate model)
        {
            var entity =
                new DeckCard()
                {
                    OwnerId = _userId,
                    CardId = model.CardId,
                    DeckId = Convert.ToInt32(model.DeckId),
                    Quantity = model.Quantity
                };
            _ctx.DeckCards.Add(entity);
            return _ctx.SaveChanges() == 1;
        }

        public IEnumerable<DeckCardDetail> GetDeckCardsByDeckId(int id)
        {
            var query =
                _ctx
                .DeckCards
                .Where(e => e.DeckId == id && e.OwnerId == _userId)
                .Select(
                    e =>
                    new DeckCardDetail
                    {
                        DeckCardId = e.DeckCardId,
                        CardId = e.CardId,
                        DeckId = e.DeckId,
                        CardName = e.Card.CardName,
                        CardType = e.Card.CardType,
                        Quantity = e.Quantity
                    }
                    );
            return query.ToArray();
        }

        public DeckCardDetail GetDeckCardById(int id)
        {
            var entity =
                _ctx
                    .DeckCards
                    .Single(e => e.DeckCardId == id && e.OwnerId == _userId);
            DeckCardDetail detail =
                new DeckCardDetail
                {
                    DeckCardId = entity.DeckCardId,
                    DeckId = entity.DeckId,
                    CardId = entity.CardId,
                    CardName = entity.Card.CardName,
                    CardType = entity.Card.CardType,
                    Quantity = entity.Quantity
                };
            return detail;
        }

        public bool UpdateDeckCard(DeckCardEdit model)
        {
            var entity =
                _ctx
                    .DeckCards
                    .Single(e => e.DeckCardId == model.DeckCardId && e.OwnerId == _userId);

            entity.DeckCardId = model.DeckCardId;
            entity.DeckId = model.DeckId;
            entity.CardId = model.CardId;
            entity.Quantity = model.Quantity;

            return _ctx.SaveChanges() == 1;
        }

        public bool DeleteDeckCard(int deckCardId)
        {
            var entity =
                _ctx
                    .DeckCards
                    .Single(e => e.DeckCardId == deckCardId && e.OwnerId == _userId);
            _ctx.DeckCards.Remove(entity);

            return _ctx.SaveChanges() == 1;
        }
    }
}
