using Books.Entity;
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
        Task UpdateBookAsync(int id, Book book);
        Task DeleteBookAsync(int id);
    }
}
