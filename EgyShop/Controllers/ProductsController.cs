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
using EgyShop.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using EgyShop.Infastructure;
using Microsoft.AspNetCore.Authorization;

namespace EgyShop.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly StoreRepository _storeRepository;
        private readonly ProductRepository _productRepository;


        public ProductsController(ApplicationDbContext context, UserManager<AppUser> userManager, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            webHostEnvironment = hostEnvironment;
            _storeRepository = new StoreRepository(context);
            _productRepository = new ProductRepository(context, hostEnvironment);

        }

        //Get Store Products
        public async Task<IActionResult> Index()
        {
            AppUser appUser = await _userManager.GetUserAsync(User);
            var store = await _storeRepository.GetUserStoreWithCategories(appUser);
            if (store == null)
            {
                return RedirectToAction("Create", "Store");

            }
            if (store.StoreCategories == null)
            {
                return RedirectToAction("Create", "categories");
            }
            var storeCategories = store.StoreCategories;

            var products = _productRepository.GetStoreProductsWithCategories(storeCategories);
                
              
            if (products.Count == 0) {
                return RedirectToAction("Create");
            }
            TempData["ImagesFolder"] = appUser.Id;
            return View(products);
        }

        // GET: Products/Details/5
        /*public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.CategoryProducts
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }*/

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
           
            AppUser appUser = await _userManager.GetUserAsync(User);
            var store = await _storeRepository.GetUserStoreWithCategories(appUser);

            if (store == null)
            {
                return RedirectToAction("Create", "Store");

            }
            if (store.StoreCategories.Count == 0) {
                return RedirectToAction("Create", "categories");
            }
            ViewData["CategoryId"] = new SelectList(store.StoreCategories, "CategoryId", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(int storeId ,[Bind("ProductId,Name,Description,PhotoLink,Price,CategoryId")] Product product)

        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser AUser = await _userManager.GetUserAsync(User);

                string imageUrl = await _productRepository.SaveFileInClouidAndReturnFileUrl(model.PhotoLink , AUser.Id);
                Product product = _productRepository.CreateProductFromProductViewModel(model , imageUrl);
                await _productRepository.SaveProductInDatabase(product);
                return RedirectToAction(nameof(Index));
            }
            AppUser appUser = await _userManager.GetUserAsync(User);
            var store = await _storeRepository.GetUserStoreWithCategories(appUser);
            ViewData["CategoryId"] = new SelectList(store.StoreCategories, "CategoryId", "Name", model.CategoryId);
            return View(model);
        }
       
      
    // GET: Products/Edit/5
    public async Task<IActionResult> Edit(int id)
        {

            var product = await _productRepository.FindPrdouctById(id);
            if (product == null)
            {
                return NotFound();
            }
            AppUser appUser = await _userManager.GetUserAsync(User);
            var store = await _storeRepository.GetUserStoreWithCategories(appUser);
            
            if (store == null)
            {
                return RedirectToAction("Create", "Store");

            }
            var category = store.StoreCategories.Find(a => a.CategoryId == product.CategoryId);
            
            if (category == null) {
                return RedirectToAction("Create");
            }
            ProductViewModel productViewModel = _productRepository.CreateProductViewModelFromProduct(product);

            ViewData["CategoryId"] = new SelectList(store.StoreCategories, "CategoryId", "Name", productViewModel.CategoryId);

            return View(productViewModel);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        //public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,Description,PhotoLink,Price,CategoryId")] Product product)
        public async Task<IActionResult> Edit(int id, ProductViewModel productViewModel )
        {
            if (id != productViewModel.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (productViewModel.PhotoLink == null)
                    {
                        Product product = _productRepository.CreateProductFromProductViewModel(productViewModel);
                        await _productRepository.UpdateProductInDatabase(product);
                    }

                    else {
                        AppUser AUser = await _userManager.GetUserAsync(User);
                      string imageurl = await _productRepository.
                            changeImageINstoarge(productViewModel.PhotoLink, productViewModel.PreviousPhotoLink , AUser.Id);
                        productViewModel.PreviousPhotoLink = imageurl;
                        Product product = _productRepository.CreateProductFromProductViewModel(productViewModel);
                        await _productRepository.UpdateProductInDatabase(product);                   
                        return RedirectToAction(nameof(Index));
                    }
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(productViewModel.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.StoreCategory, "CategoryId", "CategoryId", productViewModel.CategoryId);
            return View(productViewModel);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            
            var product = await _productRepository.GetProductWithCategory(id);
            
            if (product == null)
            {
                return NotFound();
            }

            AppUser appUser = await _userManager.GetUserAsync(User);
            var store = await _storeRepository.GetUserStoreWithCategories(appUser);

            var category = store.StoreCategories.Find(a => a.CategoryId == product.CategoryId);

            if (category == null)
            {
                return RedirectToAction("Create", "product");
            }
            TempData["ImagesFolder"] = appUser.Id;


            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            AppUser appUser = await _userManager.GetUserAsync(User);

            await _productRepository.DeleteProductFromDatabase(id , appUser.Id);
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.CategoryProducts.Any(e => e.ProductId == id);
        }
        //For Admin
        // GET: Products
        /*public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CategoryProducts.Include(p => p.Category);
            return View(await applicationDbContext.ToListAsync());
        }*/

    }
}
