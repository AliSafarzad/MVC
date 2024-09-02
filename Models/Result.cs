using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{

    public class Result
    {
        public bool Success { get; set; }
        public int Error { get; set; }
        public string Token { get; set; }
    }

    public class Result<T> : Result
    {
        public T Data { get; set; }
    }

}
