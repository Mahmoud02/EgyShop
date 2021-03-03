using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EgyShop.Models
{
    public class StoreComments
    {
        [Key]
        public int CommentId{ get; set; }
        public String Comment { get; set; }

        public String? UserID { get; set; }
        public AppUser User { get; set; }

        [Required]
        public int StoreId { get; set; }
        public Store Store { get; set; }
    }
}
