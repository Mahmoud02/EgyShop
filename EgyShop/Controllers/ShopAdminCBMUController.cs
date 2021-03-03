using EgyShop.Data;
using EgyShop.Models;
using EgyShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EgyShop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ShopAdminCBMUController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public ShopAdminCBMUController(ApplicationDbContext context, UserManager<AppUser> userManager) {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {

            var Users = await _context.Users.ToListAsync();
            var numOfRegiesterdUsers = Users.Count;

            var applicationDbContext = _context.Stores.Include(s => s.StoreType).Include(s => s.User);
            List<Store> storeList = await applicationDbContext.ToListAsync();
            var numberOfStores = storeList.Count;
            AdminViewModel adminViewModel = new AdminViewModel()
            {
                NumberOfStores = numberOfStores,
                NumberOfUsers = numOfRegiesterdUsers,
                stores = storeList
            };
            return View(adminViewModel);
        }
    }
}
