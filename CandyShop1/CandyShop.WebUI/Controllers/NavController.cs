using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CandyShop.Domain.Abstract;

namespace GameStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private ICategoryRepository categoryRepo;

        public NavController(ICategoryRepository repo)
        {
            categoryRepo = repo;
        }
        public PartialViewResult Menu(string category = null)
        {
            ViewBag.Selectedcategory = category;

            IEnumerable<String> categories = categoryRepo.Categories
                .Select(cat => cat.Name)
                .Distinct()
                .OrderBy(x => x);
            return PartialView(categories);
        }
    }
}