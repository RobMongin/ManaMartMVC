using ManaMart.Models;
using ManaMart.Models.JoinModels.OrderDeckModel;
using ManaMart.Models.OrderModels;
using ManaMart.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManaMart.Controllers
{
    public class OrderController : Controller
    {
        

        // GET: Order
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new OrderService(userId);
            var model = service.GetOrders();
            return View(model);
        }


        //Get
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateOrderService();
            if (service.CreateOrder(model))
            {
                TempData["SaveResult"] = "Your order was created.";
                return RedirectToAction("Index");
            };
            ModelState.AddModelError("", "Order could not be created.");

            return View("Index");
        }

        private OrderService CreateOrderService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new OrderService(userId);
            return service;
        }

        public ActionResult Details(int id)
        {
            var svc = CreateOrderService();
            var model = svc.GetOrderById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var svc = CreateOrderService();
            var detail = svc.GetOrderById(id);
            var model =
                new OrderEdit
                {
                    OrderId  = detail.OrderId,
                    OrderDate = detail.OrderDate,
                    PhoneNumber = detail.PhoneNumber,
                    CustomerName = detail.CustomerName
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, OrderEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.OrderId != id)
            {
                ModelState.AddModelError("", "Invalid or missing Id");
                return View(model);
            }

            var service = CreateOrderService();
            if (service.UpdateOrder(model))
            {
                TempData["SaveResult"] = "Your order was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "This order could not be updated");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateOrderService();
            var model = svc.GetOrderById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var svc = CreateOrderService();
            svc.DeleteOrder(id);

            TempData["SaveResult"] = "Your order was deleted";

            return RedirectToAction("Index");
        }

        public ActionResult OrderView(int id)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var orderService = new OrderService(userId);
            var orderDeckService = new OrderDeckService(userId);

            OrderViewModel model = new OrderViewModel();
            model.OrderDetail = orderService.EditOrderById(id);

            var deckList = orderDeckService.GetOrderDecksByDeckId(id);
            foreach (var x in deckList)
            {
                OrderDeckDetail y = new OrderDeckDetail();
                {
                    y.OrderDeckId = x.OrderDeckId;
                    y.OrderId = x.OrderId;
                    y.DeckId = x.DeckId;
                    y.DeckName = x.DeckName;
                    y.DeckType = x.DeckType;
                    
                };
                model.Decks.Add(y);
            }
            return View(model);
        }
    }
}