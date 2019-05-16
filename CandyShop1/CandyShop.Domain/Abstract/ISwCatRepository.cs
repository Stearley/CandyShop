using CandyShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandyShop.Domain.Abstract
{
    public interface ISwCatRepository
    {
        IEnumerable<SwCat> SwCat { get; }
    }
}
