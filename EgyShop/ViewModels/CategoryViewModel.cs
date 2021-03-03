using EgyShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EgyShop.ViewModels
{
    public class CategoryViewModel
    {
     
        public Store Store { get; set; }
        public Category Category { get; set; }
        public List<Category> Categoryies { get; set; }
        public List<Product> Products { get; set; }
    }
}
