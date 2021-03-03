using EgyShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EgyShop.ViewModels
{
    public class AdminViewModel
    {
        public int NumberOfStores { get; set; }
        public int NumberOfUsers { get; set; }
        public List<Store> stores { get; set; }
    }
}
