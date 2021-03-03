using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EgyShop.Data;
using EgyShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace EgyShop.Infastructure
{
    public class StoreRepository 
    {
        private readonly ApplicationDbContext _context;

        public StoreRepository(ApplicationDbContext context)
        {
            _context = context;
          
        }
        public async Task<Store> CheckIfUserHasStore(AppUser appUser)
        {

            var store = await _context.Stores
               .Where(m => m.UserID == appUser.Id)
               .FirstOrDefaultAsync();
            return store;
        }
        public async Task<Store> getStoreByID(int id)
        {

            var store = await _context.Stores
               .Where(m => m.StoreId == id)
               .Include(s => s.StoreCategories)
               .FirstOrDefaultAsync();
            return store;
        }

        public async Task<Store> GetUserStoreWithCategoriesAndType(AppUser appUser)
        {

            var store = await _context.Stores
                .Include(s => s.StoreType)
                .Include(s => s.StoreCategories)             
                .Where(m => m.UserID == appUser.Id)
                .FirstOrDefaultAsync();
            return store;
        }
        public async Task<Store> GetUserStoreWithThemeSetting(AppUser appUser)
        {

            var store = await _context.Stores
                .Include(s => s.StoreThemeSetting)          
                .Where(m => m.UserID == appUser.Id)
                .FirstOrDefaultAsync();
            return store;
        }
        public async Task<Store> GetUserStoreWithThemeSetting(int storeId)
        {

            var store = await _context.Stores
                .Include(s => s.StoreThemeSetting)
                .Where(m => m.StoreId == storeId)
                .FirstOrDefaultAsync();
            return store;
        }
        public async Task<Store> GetStoreWithCategoriesAndType(int id)
        {
            var store = await _context.Stores
               .Include(s => s.StoreType)
               .Include(s => s.StoreCategories)
               .Include(s => s.StoreThemeSetting)
               .FirstOrDefaultAsync(m => m.StoreId == id);
            return store;
        }
        public async Task<Store> GetUserStoreWithCategories(AppUser appUser)
        {

            var store = await _context.Stores
                .Include(s => s.StoreCategories)
                .FirstOrDefaultAsync(m => m.UserID == appUser.Id);
            return store;
        }
        public async Task<Store> GetUserStoreWithType(AppUser appUser)
        {

            var store = await _context.Stores
                .Include(s => s.StoreType)
                .FirstOrDefaultAsync(m => m.UserID == appUser.Id);
            return store;
        }
        
        public async Task  SaveStoreInDatabase(Store store)
        {

           _context.Add(store);
           await _context.SaveChangesAsync();
           StoreThemeSetting storeTheme = StoreThemeSetting.getSoreThemDefaultObject();
           storeTheme.StoreId = store.StoreId;
           _context.Add(storeTheme);
           await _context.SaveChangesAsync();

        }
        public async Task UpdateStoreInDatabase(Store store)
        {

            _context.Update(store);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteStoreFromDatabase(Store store)
        {

            _context.Stores.Remove(store);
            await _context.SaveChangesAsync();
        }

    }
}
