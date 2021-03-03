using EgyShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EgyShop.ViewModels
{
    public class ShowPrdouctViewModel
    {
        public Product Product { get; set; }
        public Store Store { get; set; }
        public Category Category { get; set; }
        public List<Product> SameProducts { get; set; } 
       
    }
}
