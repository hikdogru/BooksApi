using Books.Core.Data.Ef;
using Books.Data.Abstract;
using Books.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Data.Concrete.Ef
{
    public class EfBookRepository : EfEntityRepositoryBase<Book, BookContext>, IBookRepository
    {
        private BookContext _context;
        public EfBookRepository(BookContext context):base(context)
        {
            _context = context;
        }

        public async Task PatchUpdateAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
