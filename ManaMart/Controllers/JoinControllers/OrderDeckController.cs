using ManaMart.Models.DeckModels;
using ManaMart.Models.JoinModels.OrderDeckModel;
using ManaMart.Models.OrderModels;
using ManaMart.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManaMart.Controllers.JoinControllers
{
    public class OrderDeckController : Controller
    {
        // GET: OrderDeck
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Create()
        {
            ViewBag.Title = "New Order Deck";

            var userId = Guid.Parse(User.Identity.GetUserId());
            var deckService = new DeckService(userId);
            var orderService = new OrderService(userId);

            List<OrderListItem> orders = orderService.GetOrders().ToList();
            var queryOrder= from o in orders
                            select new SelectListItem()
                            {
                                Value = o.OrderId.ToString()
                                
                            };
            ViewBag.CharacterId = queryOrder.ToList();

            List<DeckListItem> decks = deckService.GetDecks().ToList();
            var queryDeck = from o in decks
                            select new SelectListItem()
                            {
                                Value = o.DeckId.ToString(),
                                Text = o.DeckName
                            };
            ViewBag.ItemId = queryDeck.ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderDeckCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateOrderDeckService();
            if (service.CreateOrderDeck(model))
            {
                TempData["SaveResult"] = "Your deck was added.";
                return RedirectToAction("OrderView", "Order", new { id = model.OrderId });
            };

            ModelState.AddModelError("", "Deck could not be added.");

            return View("Index");
        }

        private OrderDeckService CreateOrderDeckService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new OrderDeckService(userId);
            return service;
        }

        public ActionResult Edit(int id)
        {
            var svc = CreateOrderDeckService();
            var detail = svc.GetOrderDeckById(id);
            var model =
                new OrderDeckEdit
                {
                    OrderDeckId = detail.OrderDeckId,
                    OrderId = detail.OrderId,
                    DeckId = detail.DeckId
                    
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, OrderDeckEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.DeckId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            var service = CreateOrderDeckService();
            if (service.UpdateOrderDeck(model))
            {
                TempData["SaveResult"] = "Updated.";
                return RedirectToAction("OrderView", "Order", new { id = model.OrderId });
            }
            ModelState.AddModelError("", "Your request could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateOrderDeckService();
            var model = svc.GetOrderDeckById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var svc = CreateOrderDeckService();
            svc.DeleteOrderDeck(id);

            TempData["SaveResult"] = "Your card was deleted";

            return RedirectToAction("Index");
        }
    }
}