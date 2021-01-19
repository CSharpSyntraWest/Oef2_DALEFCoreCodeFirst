using School.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Oef2_DALEFCoreCodeFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            using (SchoolDbContext db = new SchoolDbContext())
            {
                db.DbScholen.Add(new DbSchool() { Naam = "Syntra-West" });
                db.SaveChanges();
                List<DbSchool> scholen = db.DbScholen.ToList();
                foreach (DbSchool dbSchool in scholen)
                {
                    Console.WriteLine($"{dbSchool.SchoolId} : {dbSchool.Naam}");
                }
            }
            Console.ReadKey();
        }
    }
}
