using Books.Entity;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Business.Abstract
{
    public interface IBookService
    {
        Task<List<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(int id);

        Task AddBookAsync(Book book);
        Task<bool> UpdateBookAsync(int id, Book book);
        Task DeleteBookAsync(int id);
        Task<bool> PatchUpdateAsync(int id, JsonPatchDocument<Book> book);

        Task<List<Book>> SortByAsync(string sortByName, string sortById);
        Task<List<Book>> SearchAsync(string q, List<Book> books);
    }
}
