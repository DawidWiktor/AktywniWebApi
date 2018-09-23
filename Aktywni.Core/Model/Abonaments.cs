using System;

namespace Aktywni.Core.Model
{
    public partial class Abonaments
    {
        public int AbonamentId { get; set; }
        public int UserId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public decimal Price { get; set; }
        public int? OrderId { get; set; }
        public bool? IsPaid { get; set; }

        public Users User { get; set; }
    }
}