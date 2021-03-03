using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EgyShop.Models
{
    public class AppUser : IdentityUser
    {
        public Store Store { get; set; }
        public List<StoreComments> StoreComments { get; set; }

    }
}
