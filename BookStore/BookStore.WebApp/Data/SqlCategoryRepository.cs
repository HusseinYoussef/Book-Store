using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.WebApp.Data
{
    public class SqlCategoryRepository : ICategoryRepository
    {
        private readonly BookStoreDbContext _context;

        public SqlCategoryRepository(BookStoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            IEnumerable<Category> categories = await _context.Categories.OrderBy(c => c.Id).ToListAsync();
            return categories;
        }

        public async Task<IEnumerable<Category>> GetCategoriesById(IEnumerable<int> ids)
        {
            IEnumerable<Category> categories = await _context.Categories.Where(c => ids.Contains(c.Id))
                                                                        .ToListAsync();
            return categories;
        }
    }
}