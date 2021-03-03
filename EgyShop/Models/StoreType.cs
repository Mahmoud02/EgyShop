using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EgyShop.Models
{
    public class StoreType
    {
        public int StoreTypeId { get; set; }
        public String Type { get; set; }

        public List<Store> Stores { get; set; }


    }
}
