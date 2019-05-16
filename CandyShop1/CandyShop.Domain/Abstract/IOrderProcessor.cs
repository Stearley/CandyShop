using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CandyShop.Domain.Entities;
using System.Threading.Tasks;

namespace CandyShop.Domain.Abstract
{
    public interface IOrderProcessor
    {
        void ProcessOrder(Cart cart, ShippingDetails shippingDetails);
    }
}
