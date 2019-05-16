using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CandyShop.Domain.Abstract;
using CandyShop.Domain.Entities;

namespace CandyShop.Domain.Concrete
{
    public class EFByerRepository : IByerRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Byer> Byers
        {
            get { return context.Byers; }
        }

        public void SaveByer(Byer byer)
        {
            if (byer.Id == 0) context.Byers.Add(byer);
            else
            {
                Byer dbEntry = context.Byers.Find(byer.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = byer.Name;
                    dbEntry.Email = byer.Email;
                    dbEntry.Adress = byer.Adress;
                }
            }
            context.SaveChanges();
        }
    }
}
