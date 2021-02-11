using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.WebApp.Models;

namespace BookStore.WebApp.Data
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<IEnumerable<Category>> GetCategoriesById(IEnumerable<int> ids);
    }
}