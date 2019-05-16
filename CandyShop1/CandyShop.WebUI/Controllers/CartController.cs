using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CandyShop.Domain.Entities;
using CandyShop.Domain.Abstract;
using CandyShop.WebUI.Models;


namespace CandyShop.WebUI.Controllers
{
    public class CartController : Controller
    {
        private ISweetRepository repository;
        private IOrderProcessor orderProcessor;
        public CartController (ISweetRepository repo, IOrderProcessor processor)
        {
            repository = repo;
            orderProcessor = processor;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int id, string returnUrl)
        {
            Sweet sweet = repository.Sweets
                .FirstOrDefault(g => g.Id == id);

            if (sweet != null)
            {
                cart.AddItem(sweet, 1);
            }

            return RedirectToAction("Index", new { returnUrl});
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int id, string returnUrl)
        {
            Sweet sweet = repository.Sweets
                .FirstOrDefault(g => g.Id == id);

            if (sweet != null)
            {
                cart.RemoveLine(sweet);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Извините, ваша корзина пуста!");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }
    }
}