using ManaMart.Models.CardModels;
using ManaMart.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManaMart.Controllers
{
    public class CardController : Controller
    {
        // GET: Item
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CardService(userId);
            var model = service.GetCards();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CardCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateCardService();
            if (service.CreateCard(model))
            {
                TempData["SaveResult"] = "Your card was created.";
                return RedirectToAction("Index");
            };
            ModelState.AddModelError("", "Card could not be created.");

            return View("Index");

        }

        private CardService CreateCardService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CardService(userId);
            return service;
        }

        public ActionResult Details(int id)
        {
            var svc = CreateCardService();
            var model = svc.GetCardById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var svc = CreateCardService();
            var detail = svc.GetCardById(id);
            var model =
                new CardEdit
                {
                    CardId = detail.CardId,
                    CardName = detail.CardName,
                    CardType = detail.CardType,
                    ManaCost = detail.ManaCost,
                    ManaType = detail.ManaType
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CardEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if(model.CardId != id)
            {
                ModelState.AddModelError("", "Invalid or missing Id");
                return View(model);
            }

            var service = CreateCardService();
            if (service.UpdateCard(model))
            {
                TempData["SaveResult"] = "Your card was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "This card could not be updated");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateCardService();
            var model = svc.GetCardById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var svc = CreateCardService();
            svc.DeleteCard(id);

            TempData["SaveResult"] = "Your card was deleted";

            return RedirectToAction("Index");
        }


    }
}