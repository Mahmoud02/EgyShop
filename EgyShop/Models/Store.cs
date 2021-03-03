using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace EgyShop.Models
{
    public class Store
    {
        public int StoreId { get; set; }
        [Required(ErrorMessage = "لا يمكن أن ترك هذا الحقل فارغاً")]
        [StringLength(100)]
        [Display(Name = "الأسم")]
        public String Name { get; set; }

        [Required(ErrorMessage = "لا يمكن أن ترك هذا الحقل فارغاً")]
        [Display(Name = "الوصف")]
        public String Description { get; set; }

        [Required(ErrorMessage = "لا يمكن أن ترك هذا الحقل فارغاً")]
        [Phone]
        [Display(Name = "رقم الهاتف المحمول")]
        public String PhoneNumber { get; set; }

        [Required(ErrorMessage = "لا يمكن أن ترك هذا الحقل فارغاً")]
        [EmailAddress]
        [Display(Name = "البريد الألكترونى")]
        public String Email { get; set; }

        [Required(ErrorMessage = "لا يمكن أن ترك هذا الحقل فارغاً")]
        [Display(Name = "البلد")]
        public String Country { get; set; }

        [Required(ErrorMessage = "لا يمكن أن ترك هذا الحقل فارغاً")]
        [Display(Name = "المدينه")]
        public String City { get; set; }

        [Display(Name = "المنطقه")]
        public String Region { get; set; }

        [Display(Name = "الطريق الرئيسي")]
        public String Street { get; set; }

        [Display(Name = " رابط الفيس بوك الخاص بك")]
        public String FacebookPageLink { get; set; }

        public String UserID{ get; set; }
        public AppUser User { get; set; }

        public List<StoreComments> StoreComments { get; set; }
        [Display(Name = "نوع المتجر الألكترونى")]

        public int StoreTypeId { get; set; }
        [Display(Name = "نوع المتجر الألكترونى")]

        public StoreType StoreType { get; set; }

        public List<Category> StoreCategories { get; set; }

        public StoreThemeSetting StoreThemeSetting { get; set; }
    }
}
