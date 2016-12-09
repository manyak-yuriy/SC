using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Pair<T1, T2>
    {
        public T1 item1 { get; set; }
        public T2 item2 { get; set; }
    }
}