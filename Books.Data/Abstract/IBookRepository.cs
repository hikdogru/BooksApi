using Books.Core.Data;
using Books.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Data.Abstract
{
    public interface IBookRepository: IEntityRepository<Book>
    {
        
    }
}
