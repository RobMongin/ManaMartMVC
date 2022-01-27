using ManaMart.Models;
using ManaMart.Models.DeckModels;
using ManaMart.Models.JoinModels.DeckCardModel;
using ManaMart.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManaMart.Controllers
{
    public class DeckController : Controller
    {
        // GET: Deck
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new DeckService(userId);
            var model = service.GetDecks();
            return View(model);
        }

        //Get
        public ActionResult Create()
        {
            return View();   
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DeckCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateDeckService();
            if (service.CreateDeck(model))
            {
                TempData["SaveResult"] = "Your deck was created.";
                return RedirectToAction("Index");
            };
            ModelState.AddModelError("", "Deck could not be created.");

            return View("Index");
        }

        private DeckService CreateDeckService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new DeckService(userId);
            return service;
        }

        public ActionResult Details(int id)
        {
            var svc = CreateDeckService();
            var model = svc.GetDeckById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var svc = CreateDeckService();
            var detail = svc.GetDeckById(id);
            var model =
                new DeckEdit
                {
                    DeckId = detail.DeckId,
                    DeckName = detail.DeckName,
                    DeckType = detail.DeckType
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, DeckEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.DeckId != id)
            {
                ModelState.AddModelError("", "Invalid or missing Id");
                return View(model);
            }

            var service = CreateDeckService();
            if (service.UpdateDeck(model))
            {
                TempData["SaveResult"] = "Your deck was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "This deck could not be updated");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateDeckService();
            var model = svc.GetDeckById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var svc = CreateDeckService();
            svc.DeleteDeck(id);

            TempData["SaveResult"] = "Your deck was deleted";

            return RedirectToAction("Index");
        }

        public ActionResult DeckView(int id)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var deckService = new DeckService(userId);
            var deckCardService = new DeckCardService(userId);

            DeckViewModel model = new DeckViewModel();
            model.DeckDetail = deckService.EditDeckById(id);

            var cardList = deckCardService.GetDeckCardsByDeckId(id);
            foreach (var x in cardList)
            {
                DeckCardDetail y = new DeckCardDetail();
                {
                    y.DeckCardId = x.DeckCardId;
                    y.DeckId = x.DeckId;
                    y.CardId = x.CardId;
                    y.CardName = x.CardName;
                    y.CardType = x.CardType;
                    y.Quantity = x.Quantity;
                };
                model.Cards.Add(y);
            }
            return View(model);
        }
    }
}
