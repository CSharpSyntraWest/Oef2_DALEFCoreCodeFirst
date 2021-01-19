using Microsoft.EntityFrameworkCore.Storage;
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
            //VoegSchoolToe("Syntra-West");
            DbSchool dbSchool = ZoekSchoolOpNaam("Syntra-West");
            if(dbSchool == null)
            {
                Console.WriteLine("School 'Syntra-West' niet gevonden");
                return;
            }
            DbStudent dbStudent = new DbStudent() { Voornaam = "Jan", Familienaam = "Jansens", 
                GeboorteDatum = new DateTime(1990, 1, 1) };
            DbVak vak1 = new DbVak() { Naam = "C#", AantalLesuren = 100 };
            DbVak vak2 = new DbVak() { Naam = "DataBase", AantalLesuren = 80 };
            VoegStudentToeMetVakken(dbSchool, dbStudent,vak1,vak2); 
            Console.ReadKey();
        }

        private static void VoegStudentToeMetVakken(DbSchool dbSchool, DbStudent dbStudent, params DbVak[] vakken)
        {
            using (SchoolDbContext db = new SchoolDbContext())
            {
                using (IDbContextTransaction transaction = db.Database.BeginTransaction())
                {
                    try {
                        dbStudent.Vakken = new List<DbVak>();
                        foreach (DbVak dbVak in vakken) dbStudent.Vakken.Add(dbVak);
                        db.DbStudenten.Add(dbStudent);
                        dbSchool.Studenten = new List<DbStudent>();
                        dbSchool.Studenten.Add(dbStudent);
                        db.DbScholen.Update(dbSchool);
                        db.SaveChanges();
                        transaction.Commit();
                    }catch(Exception e) {
                        Console.WriteLine("Kon student niet toevoegen:" + e.Message);
                        transaction.Rollback();
                    }
                }
            }
        }

        private static DbSchool ZoekSchoolOpNaam(string naam)
        {          
            using (SchoolDbContext db = new SchoolDbContext())
            {
                DbSchool school = db.DbScholen.Where(e => e.Naam.ToLower()==naam.ToLower()).FirstOrDefault();
                return school;
            }       
        }

        private static void VoegSchoolToe(string schoolNaam)
        {
            using (SchoolDbContext db = new SchoolDbContext())
            {

                db.DbScholen.Add(new DbSchool() { Naam = schoolNaam });
                db.SaveChanges();
                List<DbSchool> scholen = db.DbScholen.ToList();
                foreach (DbSchool dbSchool in scholen)
                {
                    Console.WriteLine($"{dbSchool.SchoolId} : {dbSchool.Naam}");
                }
            }
        }

     
    }
}
