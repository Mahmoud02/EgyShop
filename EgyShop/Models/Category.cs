using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EgyShop.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "لا يمكنك أن تترك هذا الحقل فارغاً")]
        [Display(Name = "الأسم")]
        public String Name { get; set; }
        [Required(ErrorMessage = "لا يمكنك أن تترك هذا الحقل فارغاً")]
        [Display(Name = "الوصف")]
        public String Description { get; set; }

        [Required]
        public int StoreId { get; set; }
        public Store Store { get; set; }

        public List<Product> CategoryProducts { get; set; }

    }
}
