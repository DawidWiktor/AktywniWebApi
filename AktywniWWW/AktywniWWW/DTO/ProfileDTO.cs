using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AktywniWWW.DTO
{
    public class ProfileDTO
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Role { get; set; }
        public Abonament LastAbonament { get; set; }
        public List<Abonament> ListAbonaments { get; set; }
    }
}