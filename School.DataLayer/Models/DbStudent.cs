using System.Collections.Generic;

namespace School.DataLayer.Models
{
    public class DbStudent:DbPersoon
    {
        public IList<DbVak> Vakken { get; set; }
    }
}