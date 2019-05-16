using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CandyShop.Domain.Abstract;
using CandyShop.Domain.Entities;

namespace CandyShop.Domain.Concrete
{
    public class EFCategoryRepository : ICategoryRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Category> Categories
        {
            get { return context.Categories; }
        }
    }
}
