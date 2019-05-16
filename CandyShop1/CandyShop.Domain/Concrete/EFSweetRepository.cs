﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CandyShop.Domain.Abstract;
using CandyShop.Domain.Entities;

namespace CandyShop.Domain.Concrete
{
    public class EFSweetRepository : ISweetRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Sweet> Sweets
        {
            get { return context.Sweets; }
        }

        public void SaveSweet(Sweet sweet)
        {
            if (sweet.Id == 0) context.Sweets.Add(sweet);
            else
            {
                Sweet dbEntry = context.Sweets.Find(sweet.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = sweet.Name;
                    dbEntry.Description = sweet.Description;
                    dbEntry.Price = sweet.Price;
                    dbEntry.ImageData = sweet.ImageData;
                    dbEntry.ImageMimeType = sweet.ImageMimeType;
                }
            }
            context.SaveChanges();
        }

        public Sweet DeleteSweet(int id)
        {
            Sweet dbEntry = context.Sweets.Find(id);
            if (dbEntry != null)
            {
                context.Sweets.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
