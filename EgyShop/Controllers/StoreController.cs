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
    public class StoreController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly StoreRepository _storeRepository;
    

      public StoreController(ApplicationDbContext context , UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _storeRepository = new StoreRepository(context);
        }

        
        public async Task<IActionResult> Index()
        {
            AppUser appUser = await _userManager.GetUserAsync(User);

            Store store = await _storeRepository.GetUserStoreWithCategoriesAndType(appUser);

            if (store == null)
            {
                return RedirectToAction("Create", "Store");

            }
           
            ViewData["StoreTypeId"] = new SelectList(_context.StoreType, "StoreTypeId", "Type", store.StoreTypeId);

            return View(store);
        }

        // GET: Store/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var store = await _context.Stores
                .Include(s => s.StoreType)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.StoreId == id);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // GET: Store/Create
        public async Task<IActionResult> Create()
        {
            AppUser appUser = await _userManager.GetUserAsync(User);

            Store store = await _storeRepository.CheckIfUserHasStore(appUser);


            if (store != null)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewData["StoreTypeId"] = new SelectList(_context.StoreType, "StoreTypeId", "Type");
            return View();
        }

        // POST: Store/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StoreId,Name,Description,Email,PhoneNumber,FacebookPageLink,Country,City,Region,Street,UserID,StoreTypeId")] Store store)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = await _userManager.GetUserAsync(User);
                store.UserID = appUser.Id;
                await _storeRepository.SaveStoreInDatabase(store);
                return RedirectToAction(nameof(Index));
            }
            ViewData["StoreTypeId"] = new SelectList(_context.StoreType, "StoreTypeId", "Type");

            return View(store);
        }

        // GET: Store/Edit/5
        public async Task<IActionResult> Edit()
        {

            AppUser appUser = await _userManager.GetUserAsync(User);

            var store = await _storeRepository.GetUserStoreWithType(appUser);

            if (store == null)
            {
                return RedirectToAction(nameof(Create));
            }
            ViewData["StoreTypeId"] = new SelectList(_context.StoreType, "StoreTypeId", "Type", store.StoreTypeId);
            return View(store);
        }

        // POST: Store/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("StoreId,Name,Description,Email,PhoneNumber,FacebookPageLink,Country,City,Region,Street,UserID,StoreTypeId")] Store store)
        {
           /* if (id != store.StoreId)
            {
                return NotFound();
            }*/

            if (ModelState.IsValid)
            {
                try
                {
                    await _storeRepository.UpdateStoreInDatabase(store);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoreExists(store.StoreId))
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
            ViewData["StoreTypeId"] = new SelectList(_context.StoreType, "StoreTypeId", "Type", store.StoreTypeId);
            return View(store);
        }

        // GET: Store/Delete/5
       /* public async Task<IActionResult> Delete()
        {
            AppUser appUser = await _userManager.GetUserAsync(User);

            var store = await _storeRepository.GetUserStoreWithType(appUser);


            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // POST: Store/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed()
        {
            AppUser appUser = await _userManager.GetUserAsync(User);

            var store = await _storeRepository.CheckIfUserHasStore(appUser);
            if (store == null)
            {
                return NotFound();
            }
            await _storeRepository.DeleteStoreFromDatabase(store);
          
            return RedirectToAction(nameof(Index));
        }*/

        private bool StoreExists(int id)
        {
            return _context.Stores.Any(e => e.StoreId == id);
        }

        //for Admin
        // GET: Stores
        /*public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Stores.Include(s => s.StoreType).Include(s => s.User);
            return View(await applicationDbContext.ToListAsync());
        }*/
    }
}
