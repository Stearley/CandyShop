using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CandyShop.Domain.Entities;

namespace CandyShop.Domain.Abstract
{
    public interface IByerRepository
    {
        IEnumerable<Byer> Byers { get; }
        void SaveByer(Byer byer);
    }
}
