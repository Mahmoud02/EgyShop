using EgyShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EgyShop.ViewModels
{
    public class ShopViewModel
    {
        public Store store { get; set; }
        // public List<Category> ShopCategories { get; set; }
        public List<Product> ShopProducts { get; set; }

        public List<Product> getFeatureProducts (){
            return ShopProducts.Where(p => p.FeatureProduct == true).ToList();
        }
        public List<Product> getRecommendProducts()
        {
            return ShopProducts.Where(p => p.RecommendProduct == true).ToList();
        }
        public List<Product> getCategoryProducts(int id )
        {
            return ShopProducts.Where(p => p.CategoryId == id).ToList();
        }
    }
}
