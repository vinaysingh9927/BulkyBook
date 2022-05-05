﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models.ViewModels
{
    public class ShoppingCartVM
    {  
        public IEnumerable<ShoppingCart> ListCart { get; set; }
        //public double CartTotal { get; set; } //Remove that bcz it consist in OrderHeader
         
        public OrderHeader OrderHeader { get; set; } 
    }
}