using EgyShop.Data;
using EgyShop.Infastructure;
using EgyShop.Models;
using EgyShop.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EgyShop.Controllers
{
    public class ShopController : Controller
    {
        // GET: ShopController
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly StoreRepository _storeRepository;
        private readonly ProductRepository _productRepository;
        private readonly CategoryRepository _categoryRepository;


        public ShopController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _storeRepository = new StoreRepository(context);
            _productRepository = new ProductRepository(context);
            _categoryRepository = new CategoryRepository(context);
        }
        public async Task<IActionResult> Index(int id )
        {

            var store = await _storeRepository.GetStoreWithCategoriesAndType(id);
            if (store == null)
            {
                return NotFound();
            }

            var storeCategories = store.StoreCategories;

            var products = _productRepository.GetStoreProductsWithCategories(storeCategories);
            var shopViewModel = new ShopViewModel
            {
                store =store,
                ShopProducts = products
            };
            TempData["ImagesFolder"] = store.UserID;

            return View(shopViewModel);
        }
        public async Task<ActionResult> Product(int id) {
            var product = await _productRepository.GetProductWithCategory(id);
            if (product == null) {
                return NotFound();
            }
            var category = product.Category;
            var storeID = category.StoreId;

            var store = await _storeRepository.GetUserStoreWithThemeSetting(storeID);
            store.StoreCategories = _categoryRepository.GetCategoriesByStoreId(store.StoreId);
            var sameProducts = _productRepository.GetCategoryProducts(category.CategoryId);
            sameProducts.Remove(product);

            ShowPrdouctViewModel showPrdouctViewModel = new ShowPrdouctViewModel
            {
                Store = store,
                Category =category,
                Product = product,
                SameProducts = sameProducts
            };
            TempData["ImagesFolder"] = store.UserID;
            return View(showPrdouctViewModel);
        }
        public async Task<ActionResult> Category(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdIncludeStoreAndProducts(id);
            if (category == null)
            {
                return NotFound();
            }
            var store = category.Store;
            var storeCategories = _categoryRepository.GetCategoriesByStoreId(store.StoreId);
            var products = category.CategoryProducts;


            CategoryViewModel categoryViewModel = new CategoryViewModel
            {
                Store = store,
                Category = category,
                Categoryies = storeCategories,
                Products = products,
            };
            TempData["ImagesFolder"] = store.UserID;
            return View(categoryViewModel);
        }

        // GET: ShopController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ShopController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShopController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ShopController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ShopController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ShopController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ShopController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
