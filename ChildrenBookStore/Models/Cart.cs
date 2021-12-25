using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildrenBookStore.Models
{
    public class Cart
    {
        public int Cart_Id { get; set; }
        public string BookImage { get; set; }
        public string BookName { get; set; }
        public decimal Price{ get; set; }
        public int Book_id { get; set; }
    }
}
