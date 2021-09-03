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
        public EfBookRepository(BookContext context):base(context)
        {

        }
    }
}
