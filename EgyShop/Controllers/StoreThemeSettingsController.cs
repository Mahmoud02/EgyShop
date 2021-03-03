using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EgyShop.Data;
using EgyShop.Models;
using EgyShop.Infastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace EgyShop.Controllers
{
    [Authorize]
    public class StoreThemeSettingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly StoreRepository _storeRepository;


        public StoreThemeSettingsController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _storeRepository = new StoreRepository(context);
        }
        // GET: StoreThemeSettings/Edit/5
        public async Task<IActionResult> Edit( )
        {
            AppUser appUser = await _userManager.GetUserAsync(User);
            Store store = await _storeRepository.GetUserStoreWithThemeSetting(appUser);
            var storeTheme = store.StoreThemeSetting;
          
            return View(storeTheme);
        }

        // POST: StoreThemeSettings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,BrandColor,NavabarBackgroundColor,NabarTextColor,NavabarIconColor,NavabrIconToggl,StoreDescriotionTextColor,StoreDesctiptionToggle,FeatureProductsToggle,RecoomendProductsToggle,CarsoulToggle,NumberOfProductsPerRow,ShowProductImage,ShowProductName,ShowProductPrice,ShowProductDescription,ShowProductCategory,FooterBackgroundColor,FooterTextColor,FooterIconColo,ToggleFooterIcon,StoreId")] StoreThemeSetting storeThemeSetting)
        {
            var x = storeThemeSetting;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(storeThemeSetting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoreThemeSettingExists(storeThemeSetting.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Edit));
            }
           
            return RedirectToAction(nameof(Edit));
        }

        // GET: StoreThemeSettings/Delete/5
       

        private bool StoreThemeSettingExists(int id)
        {
            return _context.StoreThemeSettings.Any(e => e.Id == id);
        }
    }
}
