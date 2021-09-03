using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Api.Models
{
    public class BookContext : DbContext
    {
        public DbSet<Book> BookApi { get; set; }

        public BookContext(DbContextOptions<BookContext> options):base(options)
        {

        }
    }
}
