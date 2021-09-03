using Books.Api.Models;
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
        private readonly BookContext _context;

        public BookController(BookContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<List<Book>> Get()
        {
            var books = await _context.BookApi.FromSqlRaw("select Id, Name, Author, Publisher, Image from bookapi").ToListAsync();

            //var client = new RestClient("https://my-json-server.typicode.com/hikdogru/angular-app/products");
            //client.Timeout = -1;
            //var request = new RestRequest(Method.GET);
            //IRestResponse response = await client.ExecuteAsync(request);

            //var content = response.Content;
            //var jsonContent = await JsonSerializer.DeserializeAsync<List<Book>>(new MemoryStream(Encoding.UTF8.GetBytes(content)), new JsonSerializerOptions
            //{
            //    PropertyNameCaseInsensitive = true
            //});
            return books;
        }

        [HttpGet("id")]
        public async Task<IActionResult> Get(int id)
        {
            var book = await _context.BookApi.FindAsync(id);

            if (book == null)
                NotFound();


            return Ok(book);
        }

        [HttpPost]
        public IActionResult Post(Book book)
        {
            if (ModelState.IsValid)
            {
                _context.BookApi.Add(new Book { Author = book.Author, Name = book.Name, Image = book.Image, Publisher = book.Publisher });
                _context.SaveChanges();
            }

            else
                return BadRequest("Invalid data");

            return Ok();

        }
        [HttpPut("id")]
        public async Task<IActionResult> Put(int id, Book book)
        {
            if (id != book.Id)
            {
                BadRequest("Invalid id");
            }

            _context.Entry(book).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _context.BookApi.FindAsync(id);

            if (book == null)
                return NotFound();


            _context.BookApi.Remove(book);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
