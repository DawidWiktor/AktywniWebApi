using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AktywniWWW.DTO
{
    public class Abonament
    {
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public decimal Price { get; set; }
        public int OrderId { get; set; }
    }
}