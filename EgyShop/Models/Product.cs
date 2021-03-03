using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EgyShop.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required]
        [Display(Name = "الأسم")]
        public String Name { get; set; }
        [Required]
        [Display(Name = "الوصف")]
        public String Description { get; set; }
        [Display(Name = "الصوره")]
        public String PhotoLink { get; set; }
        [Display(Name = "السعر")]
        public int Price { get; set; }
        [Display(Name = "مميز")]
        public bool FeatureProduct { get; set; }
        [Display(Name = "مقترح")]
        public bool RecommendProduct { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Display(Name = "التصنيف")]
        public Category Category { get; set; }
    }
}
