using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CandyShop.Domain.Abstract;
using CandyShop.Domain.Entities;

namespace CandyShop.Domain.Concrete
{
    public class EFOrderRepository : IOrderRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Order> Orders
        {
            get { return context.Orders; }
        }

        public void SaveOrder(Order order)
        {
            if (order.Id == 0) context.Orders.Add(order);
            else
            {
                Order dbEntry = context.Orders.Find(order.Id);
                if (dbEntry != null)
                {
                    dbEntry.IdB = order.IdB;
                    dbEntry.IdS = order.IdS;
                }
            }
            context.SaveChanges();
        }
    }
}
