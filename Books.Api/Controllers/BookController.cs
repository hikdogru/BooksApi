using Books.Business.Abstract;
using Books.Data.Concrete.Ef;
using Books.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Books.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(BookContext context, IBookService bookService)
        {
            _bookService = bookService;
        }


        [HttpGet]
        public async Task<List<Book>> Get()
        {
            return await _bookService.GetAllBooksAsync();
        }

        [HttpGet("id")]
        public async Task<IActionResult> Get(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);

            if (book == null)
                return NotFound();

            return Ok(book);
        }

        //[HttpPost]
        //public IActionResult Post(Book book)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.BookApi.Add(new Book { Author = book.Author, Name = book.Name, Image = book.Image, Publisher = book.Publisher });
        //        _context.SaveChanges();
        //    }

        //    else
        //        return BadRequest("Invalid data");

        //    return Ok();

        //}
        [HttpPut("id")]
        public async Task<IActionResult> Put(int id, Book book)
        {
            if (id != book.Id)
                BadRequest("Invalid id");


            await _bookService.UpdateBookAsync(id, book);
            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);

            if (book == null)
                return NotFound();


            await _bookService.DeleteBookAsync(id);
            return NoContent();
        }
    }
}
