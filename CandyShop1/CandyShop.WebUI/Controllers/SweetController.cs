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
    public class SweetController : Controller
    {
        private ISweetRepository sweetRepo;
        private ISwCatRepository swCatRepo;
        private ICategoryRepository categoryRepo;
        public int pageSize = 4;
        public SweetController(ISweetRepository repo1, ISwCatRepository repo2, ICategoryRepository repo3)
        {
            sweetRepo = repo1;
            swCatRepo = repo2;
            categoryRepo = repo3;
        }

        public ViewResult List(string name, string category, int page = 1)
        {
            var resultList = sweetRepo.Sweets
                .Join(swCatRepo.SwCat,
                p => p.Id,
                t => t.IdS,
                (p, t) => new { Id = p.Id, Name = p.Name, Price = p.Price, IdC = t.IdC })
                .Join(categoryRepo.Categories,
                p => p.IdC,
                t => t.Id,
                (p, t) => new { Id = p.Id, Name = p.Name, Price = p.Price, Category = t.Name }
                ).ToList();

            var IdSofCategory = resultList.Where(c => c.Category == category)
                .Select(p => p.Id);

            SweetListViewModel model = new SweetListViewModel
            {
                Sweets = sweetRepo.Sweets
                .Where(x => (category == null || IdSofCategory.Contains(x.Id)) && (name == null || x.Name.StartsWith(name)))
                 .OrderBy(sweet => sweet.Id)
                 .Skip((page - 1) * pageSize)
                 .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = ((category == null) && (name == null)) ?
                    sweetRepo.Sweets.Count() :
                    category == null ? sweetRepo.Sweets.Where(sweet => sweet.Name == name).Count() :
                    sweetRepo.Sweets.Where(sweet => IdSofCategory.Contains(sweet.Id)).Count()
                },
                CurrentCategory = category,
                Name = name
            };
            return View(model);
        }

        public FileContentResult GetImage(int id)
        {
            Sweet sweet = sweetRepo.Sweets
                .FirstOrDefault(s => s.Id == id);

            if (sweet != null)
            {
                return File(sweet.ImageData, sweet.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}