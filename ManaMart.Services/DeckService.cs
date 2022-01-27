using ManaMart.Data;
using ManaMart.Models.DeckModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManaMart.Services
{
    public class DeckService
    {
        ApplicationDbContext _ctx = new ApplicationDbContext();

        private readonly Guid _userId;
        public DeckService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateDeck(DeckCreate model)
        {
            var entity =
                new Deck()
                {
                    OwnerId = _userId,
                    DeckName = model.DeckName,
                    DeckType = model.DeckType

                };
            _ctx.Decks.Add(entity);
            return _ctx.SaveChanges() == 1;
        }

        public IEnumerable<DeckListItem> GetDecks()
        {
            var query =
                _ctx
                .Decks
                .Where(e => e.OwnerId == _userId)
                .Select(
                    e =>
                    new DeckListItem
                    {
                        DeckId = e.DeckId,
                        DeckName = e.DeckName
                    }
                    );
            return query.ToArray();
        }

        public DeckDetail GetDeckById(int id)
        {
            var entity =
                _ctx
                .Decks
                .Single(e => e.DeckId == id && e.OwnerId == _userId);
            DeckDetail detail =
                new DeckDetail
                {
                    DeckId = entity.DeckId,
                    DeckName = entity.DeckName,
                    DeckType = entity.DeckType
                };
            return detail;
        }

        public DeckEdit EditDeckById(int id)
        {
            var entity =
                _ctx
                .Decks
                .Single(e => e.DeckId == id && e.OwnerId == _userId);
            return new DeckEdit
            {
                DeckId = entity.DeckId,
                DeckName = entity.DeckName,
                DeckType = entity.DeckType
            };
        }

        public bool UpdateDeck(DeckEdit model)
        {
            var entity =
                _ctx
                .Decks
                .Single(e => e.DeckId == model.DeckId && e.OwnerId == _userId);

            entity.DeckId = model.DeckId;
            entity.DeckName = model.DeckName;
            entity.DeckType = model.DeckType;

            return _ctx.SaveChanges() == 1;
        }

        public bool DeleteDeck(int deckId)
        {
            var entity =
                _ctx
                .Decks
                .Single(e => e.DeckId == deckId && e.OwnerId == _userId);
            _ctx.Decks.Remove(entity);
            return _ctx.SaveChanges() == 1;
        }

    }
}
