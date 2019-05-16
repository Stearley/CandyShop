using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CandyShop.Domain.Entities;

namespace CandyShop.WebUI.Models
{
    public class SweetListViewModel
    {
        public IEnumerable<Sweet> Sweets { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
        public string Name { get; set; }
    }
}