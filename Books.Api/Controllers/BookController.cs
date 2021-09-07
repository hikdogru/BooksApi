using AutoMapper;
using Books.Api.Dtos;
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
        private readonly IMapper _mapper;

        public BookController(BookContext context, IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }


        [HttpGet("get.{format}"), FormatFilter]
        public async Task<List<Book>> GetAll()
        {
            return await _bookService.GetAllBooksAsync();
        }

        [HttpGet("get.{format}/filter"), FormatFilter]
        public async Task<List<Book>> Filtering(string sortByName, string sortById, string q)
        {
            var books = new List<Book>();
            books = await _bookService.SortByAsync(sortByName, sortById);

            if (!string.IsNullOrEmpty(q))
            {
                books = await _bookService.SearchAsync(q, books);
            }

            return books;
        }

        [HttpGet("{id}", Name = "GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);

            if (book == null)
                return NotFound();

            return Ok(book);
        }
        // Xml and Json support
        [HttpPost("post.{format}"), FormatFilter]
        public async Task<IActionResult> Create(BookCreateDto bookCreateDto)
        {
            var bookModel = _mapper.Map<Book>(bookCreateDto);
            if (ModelState.IsValid)
                await _bookService.AddBookAsync(bookModel);

            else
                return BadRequest();

            return CreatedAtAction(nameof(GetById), new { id = bookModel.Id }, bookModel);

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BookUpdateDto bookUpdateDto)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return BadRequest();
            }

            _mapper.Map(bookUpdateDto, book);
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
        public async Task<IActionResult> PartialUpdate(int id, JsonPatchDocument<Book> patchDocument)
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
