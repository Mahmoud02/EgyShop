using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EgyShop.Data;
using EgyShop.Models;
using Microsoft.AspNetCore.Identity;
using EgyShop.Infastructure;
using Microsoft.AspNetCore.Authorization;

namespace EgyShop.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly StoreRepository _storeRepository;
        private readonly CategoryRepository _categoryRepository;


        public CategoriesController(ApplicationDbContext context , UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _storeRepository = new StoreRepository(context);
            _categoryRepository = new CategoryRepository(context);
        }

        // GET: Categories
       /* public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.StoreCategory.Include(c => c.Store);
            return View(await applicationDbContext.ToListAsync());
        }*/
        //Get:StoreCategories
       
        public async Task<IActionResult> Index()
        {
            
            AppUser appUser = await _userManager.GetUserAsync(User);

            var store = await _storeRepository.GetUserStoreWithCategories(appUser);
           
            if (store == null)
            {
                return RedirectToAction("Create", "Store");

            }
            var categories = store.StoreCategories;

            return View(categories);
        }
       

        // GET: Categories/Create
        public async Task<IActionResult> Create()
        {
            AppUser appUser = await _userManager.GetUserAsync(User);
            var store = await _storeRepository.CheckIfUserHasStore(appUser);

            if (store == null)
            {
                return RedirectToAction("Create", "Store");

            }
            TempData["storeId"] = store.StoreId;
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,Name,Description,StoreId")] Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryRepository.SaveCategoryInDatabase(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        
        public async Task<IActionResult> Edit(int?categoryId)
        {
           

            if (categoryId == null)
            {
                return RedirectToAction("Index");
            }
            AppUser appUser = await _userManager.GetUserAsync(User);
            var store = await _storeRepository.GetUserStoreWithCategories(appUser);

            if (store == null)
            {
                return RedirectToAction("Create", "Store");

            }
            //var category = await _context.StoreCategory.FindAsync(categoryId);
            var category = store.StoreCategories.Find(a => a.CategoryId == categoryId);



            if (category == null)
            {
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int categoryId, [Bind("CategoryId,Name,Description,StoreId")] Category category)
        {
            if (categoryId != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    await _categoryRepository.UpdateCategoryInDatabase(category);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
                
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
           

            var category = await _categoryRepository.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }

            AppUser appUser = await _userManager.GetUserAsync(User);
            var store = await _storeRepository.CheckIfUserHasStore(appUser);

            if (store == null)
            {
                return RedirectToAction("Create", "Store");

            }
            //var category = await _context.StoreCategory.FindAsync(categoryId);
           


            if (category.StoreId != store.StoreId)
            {
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryRepository.DeleteCategoryFromDatabase(id);
            return RedirectToAction("Index");
        }

        private bool CategoryExists(int id)
        {
            return _context.StoreCategory.Any(e => e.CategoryId == id);
        }
        // GET: Categories/Details/5
        /* public async Task<IActionResult> Details(int? id)
         {
             if (id == null)
             {
                 return NotFound();
             }

             var category = await _context.StoreCategory
                 .FirstOrDefaultAsync(m => m.CategoryId == id);
             if (category == null)
             {
                 return NotFound();
             }

             return View(category);
         }*/
    }
}
