using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildrenBookStore.Models
{
    public class Books
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public string BookType{ get; set; }
        //public string Description { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
