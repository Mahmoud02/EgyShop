using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EgyShop.Models
{
    public class StoreThemeSetting
    {
        public int Id { get; set; }
        // NavbarSetting
        public String BrandColor { get; set; }
        public String NavabarBackgroundColor { get; set; }
        public String NabarTextColor { get; set; }
    
        //Store secription Section
        public String StoreDescriotionTextColor { get; set; }
        public bool StoreDesctiptionToggle { get; set; }

        //FeatureProducts

        public bool FeatureProductsToggle { get; set; }
        //recoomenProducts
        public bool RecoomendProductsToggle { get; set; }
        //carsoul
        public bool CarsoulToggle { get; set; }
        //
        public int NumberOfProductsPerRow { get; set; }
        //Product Setting 
        public bool ShowProductImage { get; set; }
        public bool ShowProductName { get; set; }
        public bool ShowProductPrice { get; set; }
        public bool ShowProductDescription { get; set; }
        public bool ShowProductCategory { get; set; }
        //FooterSetting
        public String FooterBackgroundColor { get; set; }
        public String FooterTextColor { get; set; }
        public String FooterIconColo { get; set; }
        public bool ToggleFooterIcon { get; set; }

        public int StoreId { get; set; }
        public Store Store { get; set; }

        public static StoreThemeSetting getSoreThemDefaultObject() {
            StoreThemeSetting storeThemeSetting = new StoreThemeSetting()
            {
                BrandColor = "#FFFFFF",
                NavabarBackgroundColor = "#009970",
                NabarTextColor = "#FFFFFF",
                StoreDescriotionTextColor = "#FFFFFF",
                StoreDesctiptionToggle = true,
                FeatureProductsToggle = true,
                RecoomendProductsToggle = true,
                CarsoulToggle = true,
                NumberOfProductsPerRow = 3,
                ShowProductImage = true,
                ShowProductName = true,
                ShowProductPrice = true,
                ShowProductDescription = false,
                ShowProductCategory = false,
                FooterBackgroundColor = "#FFFFFF",
                FooterTextColor= "#000000",
                FooterIconColo = "#009970",
                ToggleFooterIcon = true
            };
            return storeThemeSetting;
        }
    }
}
