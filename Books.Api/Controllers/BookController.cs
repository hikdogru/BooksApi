using Books.Business.Abstract;
using Books.Data.Concrete.Ef;
using Books.Entity;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);

            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Book book)
        {
            if (ModelState.IsValid)
               await _bookService.AddBookAsync(book);

            else
                return BadRequest();

            return CreatedAtAction(nameof(Get), new { id = book.Id}, book);

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Book book)
        {
            if (id != book.Id)
                BadRequest("Invalid id");


            await _bookService.UpdateBookAsync(id, book);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);

            if (book == null)
                return NotFound();


            await _bookService.DeleteBookAsync(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, JsonPatchDocument<Book> patchDocument)
        {
            bool isBookExist = await _bookService.PatchUpdateAsync(id, patchDocument);
            if (isBookExist == false)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok();
        }
    }
}
