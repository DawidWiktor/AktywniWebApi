using System;

namespace Aktywni.Infrastructure.DTO.Abonament
{
    public class AbonamentDTO
    {
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public decimal Price { get; set; }
        public int OrderId { get; set; }
    }
}