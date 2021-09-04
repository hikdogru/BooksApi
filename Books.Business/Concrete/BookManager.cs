using Books.Business.Abstract;
using Books.Data.Abstract;
using Books.Entity;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Business.Concrete
{
    public class BookManager : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookManager(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task AddBookAsync(Book book)
        {
            await _bookRepository.AddAsync(book);
        }

        public async Task DeleteBookAsync(int id)
        {
            await _bookRepository.DeleteAsync(await _bookRepository.GetAsync(x => x.Id == id));
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _bookRepository.GetAllAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _bookRepository.GetAsync(x => x.Id == id);
        }

        public async Task<bool> PatchUpdateAsync(int id, JsonPatchDocument<Book> book)
        {
            var entity = await _bookRepository.GetAsync(b=>b.Id == id);
            if (entity == null)
            {
                return false;
            }

            book.ApplyTo(entity);
            await _bookRepository.PatchUpdateAsync();
            return true;
        }

        public async Task UpdateBookAsync(int id, Book book)
        {
            if (id == book.Id)
                await _bookRepository.UpdateAsync(book);
        }
    }
}
