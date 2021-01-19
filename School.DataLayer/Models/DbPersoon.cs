using System;
using System.Collections.Generic;
using System.Text;

namespace School.DataLayer.Models
{
    public class DbPersoon
    {
        public int PersoonId { get; set; }
        public string Voornaam { get; set; }
        public string Familienaam { get; set; }
        public DateTime GeboorteDatum { get; set; }
    }
}
