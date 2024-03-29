﻿using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository 
    {
        private readonly ApplicationDbContext _db;
         
        public ShoppingCartRepository(ApplicationDbContext db) : base(db) //pass db to base class also
        {
            _db = db;
        }
    }
}
