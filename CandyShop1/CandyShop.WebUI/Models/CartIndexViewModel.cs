using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CandyShop.Domain.Entities;

namespace CandyShop.WebUI.Models
{
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}