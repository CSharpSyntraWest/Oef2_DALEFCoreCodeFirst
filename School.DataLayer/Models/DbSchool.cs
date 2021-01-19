using System;
using System.Collections.Generic;
using System.Text;

namespace School.DataLayer.Models
{
    public class DbSchool
    {
        public int SchoolId { get; set; }
        public string Naam { get; set; }
        public IList<DbStudent> Studenten { get; set; }
        public IList<DbDocent> Docenten { get; set; }
    }
}
