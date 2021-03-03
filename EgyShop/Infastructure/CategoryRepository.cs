using EgyShop.Data;
using EgyShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EgyShop.Infastructure
{
    public class CategoryRepository
    {
        private readonly ApplicationDbContext _context;


        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;

        }

        public async Task<Category> GetCategoryById(int id)
        {

            var category = await _context.StoreCategory.FirstOrDefaultAsync(m => m.CategoryId == id);
            return category;
        }
        public async Task<Category> GetCategoryByIdIncludeStoreAndProducts(int id)
        {

            var store = await _context.StoreCategory
               .Where(c => c.CategoryId == id)
               .Include(c => c.Store)
               .Include(c => c.CategoryProducts)
               .FirstOrDefaultAsync();
            return store;
        }
        public List<Category> GetCategoriesByStoreId(int id)
        {

            var categories =  _context.StoreCategory
               .Where(c => c.StoreId == id)
               .ToList();
            return categories;
        }
        public async Task SaveCategoryInDatabase(Category category)
        {
            _context.Add(category);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateCategoryInDatabase(Category category)
        {

            _context.Update(category);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteCategoryFromDatabase(int id)
        {

            var category = await _context.StoreCategory.FindAsync(id);
            _context.StoreCategory.Remove(category);
            await _context.SaveChangesAsync();
        }

    }
}
