using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CandyShop.Domain.Abstract;
using CandyShop.Domain.Entities;

namespace CandyShop.WebUI.Models
{
    public class AdminInfoViewModel
    {
        ISweetRepository Sweets;
        ISwCatRepository Swcats;
    }
}