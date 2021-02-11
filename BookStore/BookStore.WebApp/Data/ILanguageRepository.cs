using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.WebApp.Models;

namespace BookStore.WebApp.Data
{
    public interface ILanguageRepository
    {
        Task<IEnumerable<Language>> GetAllLanguages();
    }
}