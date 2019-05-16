using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CandyShop.Domain.Abstract;
using CandyShop.Domain.Entities;
using CandyShop.WebUI.Models;

namespace CandyShop.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        ISweetRepository repository;

        public AdminController(ISweetRepository repo)
        {
            repository = repo;
        }
        public ActionResult Index()
        {
            return View(repository.Sweets);
        }

        public ViewResult Edit(int id)
        {
            Sweet sweet = repository.Sweets
                .FirstOrDefault(s => s.Id == id);
            return View(sweet);
        }

        [HttpPost]
        public ActionResult Edit(Sweet sweet, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    sweet.ImageMimeType = image.ContentType;
                    sweet.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(sweet.ImageData, 0, image.ContentLength);
                }
                repository.SaveSweet(sweet);
                TempData["message"] = string.Format("Изменения в товаре \"{0}\" были сохранены", sweet.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(sweet);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Sweet());
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            Sweet deletedSweet = repository.DeleteSweet((int)id);
            if (deletedSweet != null)
            {
                TempData["message"] = string.Format("Товар \"{0}\" был удалена",
                    deletedSweet.Name);
            }
            return RedirectToAction("Index");
        }
    }
}