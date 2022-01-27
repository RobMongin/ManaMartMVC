using ManaMart.Models.CardModels;
using ManaMart.Models.DeckModels;
using ManaMart.Models.JoinModels.DeckCardModel;
using ManaMart.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManaMart.Controllers.JoinControllers
{
    public class DeckCardController : Controller
    {
        // GET: DeckCard
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.Title = "New Deck Card";

            var userId = Guid.Parse(User.Identity.GetUserId());
            var deckService = new DeckService(userId);
            var cardService = new CardService(userId);

            List<DeckListItem> decks = deckService.GetDecks().ToList();
            var queryDeck = from o in decks
                                 select new SelectListItem()
                                 {
                                     Value = o.DeckId.ToString(),
                                     Text = o.DeckName
                                 };
            ViewBag.CharacterId = queryDeck.ToList();

            List<CardListItem> items = cardService.GetCards().ToList();
            var queryItem = from o in items
                            select new SelectListItem()
                            {
                                Value = o.CardId.ToString(),
                                Text = o.CardName
                            };
            ViewBag.ItemId = queryItem.ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DeckCardCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateDeckCardService();
            if (service.CreateDeckCard(model))
            {
                TempData["SaveResult"] = "Your card was added.";
                return RedirectToAction("DeckView", "Deck", new { id = model.DeckId });
            };

            ModelState.AddModelError("", "Item could not be added.");

            return View("Index");
        }

        private DeckCardService CreateDeckCardService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new DeckCardService(userId);
            return service;
        }

        public ActionResult Edit(int id)
        {
            var svc = CreateDeckCardService();
            var detail = svc.GetDeckCardById(id);
            var model =
                new DeckCardEdit
                {
                    DeckCardId = detail.DeckCardId,
                    DeckId = detail.DeckId,
                    CardId = detail.CardId,
                    Quantity = detail.Quantity
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, DeckCardEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.CardId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            var service = CreateDeckCardService();
            if (service.UpdateDeckCard(model))
            {
                TempData["SaveResult"] = "Updated.";
                return RedirectToAction("DeckView", "Deck", new { id = model.DeckId });
            }
            ModelState.AddModelError("", "Your request could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateDeckCardService();
            var model = svc.GetDeckCardById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var svc = CreateDeckCardService();
            svc.DeleteDeckCard(id);

            TempData["SaveResult"] = "Your card was deleted";

            return RedirectToAction("Index");
        }
    }
}