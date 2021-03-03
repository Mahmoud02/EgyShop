using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EgyShop.ViewModels
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "لا يمكن أن ترك هذا الحقل فارغاً")]
        [Display(Name = "الأسم")]
        public String Name { get; set; }

        [Required(ErrorMessage = "لا يمكنك أن تترك هذا الحقل فارغاً")]
        [Display(Name = "الوصف")]
        public String Description { get; set; }

        [Display(Name = "تغيير الصورة")]
        public IFormFile PhotoLink { get; set; }

        [Display(Name = "السعر")]
        public int Price { get; set; }

        [Display(Name = "اضافه إلى المنتجات امميزه")]
        public bool FeatureProduct { get; set; }

        [Display(Name = "اضافه إلى المنتجات المقترحه")]
        public bool RecommendProduct { get; set; }

        [Required(ErrorMessage = "لا يمكنك أن تترك هذا الحقل فارغاً")]
        [Display(Name = "التصنيف")]
        public int CategoryId { get; set; }
        public String PreviousPhotoLink { get; set; }

    }
}
