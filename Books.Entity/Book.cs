using Books.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Entity
{
    public class Book : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        //public string Description { get; set; }
        public string Image { get; set; }
        //public int CategoryId { get; set; }
    }
}
