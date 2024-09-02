using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class BookNewModel
    {
        public string BookName { get; set; }
        public string Nevisande { get; set; }
        public int Price { get; set; }
        public string Mozo { get; set; }
    }

    public class Book
        {
        public int IdBook { get; set; }
        public string BookName { get; set; }
        public string Nevisande { get; set; }
        public int Price { get; set; }
        public string Mozo { get; set; }
    }

    public class Book<T> : Result
    {
        public T Data { get; set; }
    }

   
}
