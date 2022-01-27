using ManaMart.Data;
using ManaMart.Models.JoinModels.OrderDeckModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManaMart.Services
{
    public class OrderDeckService
    {
        ApplicationDbContext _ctx = new ApplicationDbContext();

        private readonly Guid _userId;
        public OrderDeckService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateOrderDeck(OrderDeckCreate model)
        {
            var entity =
                new OrderDeck()
                {
                    OwnerId = _userId,
                    DeckId = model.DeckId,
                    OrderId = model.OrderId
                    
                };
            _ctx.OrderDecks.Add(entity);
            return _ctx.SaveChanges() == 1;
        }

        public IEnumerable<OrderDeckDetail> GetOrderDecksByDeckId(int id)
        {
            var query =
                _ctx
                .OrderDecks
                .Where(e => e.DeckId == id && e.OwnerId == _userId)
                .Select(
                    e =>
                    new OrderDeckDetail
                    {
                        OrderDeckId = e.OrderDeckId,
                        OrderId = e.OrderId,
                        DeckId = e.DeckId,
                        DeckName = e.Deck.DeckName,
                        DeckType = e.Deck.DeckType
                    }
                    );
            return query.ToArray();
        }

        public OrderDeckDetail GetOrderDeckById(int id)
        {
            var entity =
                _ctx
                    .OrderDecks
                    .Single(e => e.OrderDeckId == id && e.OwnerId == _userId);
            OrderDeckDetail detail =
                new OrderDeckDetail
                {
                    OrderDeckId = entity.OrderDeckId,
                    OrderId = entity.OrderId,
                    DeckId = entity.DeckId,
                    DeckName = entity.Deck.DeckName,
                    DeckType = entity.Deck.DeckType
                   
                };
            return detail;
        }

        public bool UpdateOrderDeck(OrderDeckEdit model)
        {
            var entity =
                _ctx
                    .OrderDecks
                    .Single(e => e.OrderDeckId == model.OrderDeckId && e.OwnerId == _userId);

            entity.OrderDeckId = model.OrderDeckId;
            entity.OrderId = model.OrderId;
            entity.DeckId = model.DeckId;

            return _ctx.SaveChanges() == 1;
        }

        public bool DeleteOrderDeck(int orderDeckId)
        {
            var entity =
                _ctx
                    .OrderDecks
                    .Single(e => e.OrderDeckId == orderDeckId && e.OwnerId == _userId);
            _ctx.OrderDecks.Remove(entity);

            return _ctx.SaveChanges() == 1;
        }
    }
}
