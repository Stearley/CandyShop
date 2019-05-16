using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CandyShop.Domain.Abstract;
using CandyShop.Domain.Entities;

namespace CandyShop.Domain.Concrete
{
    public class EFSwCatRepository : ISwCatRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<SwCat> SwCat
        {
            get { return context.SwCat; }
        }
    }
}
