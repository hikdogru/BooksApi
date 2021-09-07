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
        private static List<Book> _books;

        public BookManager(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
            _books = _bookRepository.GetAll();
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
            var entity = await _bookRepository.GetAsync(b => b.Id == id);
            if (entity == null)
            {
                return false;
            }

            book.ApplyTo(entity);
            await _bookRepository.PatchUpdateAsync();
            return true;
        }

        public async Task<List<Book>> SearchAsync(string q, List<Book> books)
        {
            _books = _books.Where(b => b.Name.ToLower().Contains(q.ToLower())
            || b.Author.ToLower().Contains(q)).ToList();
            return _books;
        }

        public Task<List<Book>> SortByAsync(string sortByName = "", string sortById = "")
        {
            if (sortByName == "1")
            {
                _books = _books.OrderBy(b => b.Name).ToList();
            }
            else if (sortByName == "2")
            {
                _books = _books.OrderByDescending(b => b.Name).ToList();
            }
            else if (sortById == "1")
            {
                _books = _books.OrderBy(b => b.Id).ToList();
            }
            else if(sortById == "2")
            {
                _books = _books.OrderByDescending(b => b.Id).ToList();
            }
            else
            {
                _books = _books.ToList();
            }

            return Task.Run(() => _books);
        }

        public async Task<bool> UpdateBookAsync(int id, Book book)
        {
            var entity = await _bookRepository.GetAsync(x => x.Id == id);
            if (entity != null)
            {
                await _bookRepository.UpdateAsync(book);
                return true;
            }

            return false;
        }
    }
}
