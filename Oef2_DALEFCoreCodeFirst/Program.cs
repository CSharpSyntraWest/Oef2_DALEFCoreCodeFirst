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
            DbVak vak1 = ZoekVakOpNaam("C#");
            if (vak1 == null)
            {
                vak1 = new DbVak() { Naam = "C#", AantalLesuren = 100 };
            }
            DbVak vak2 = ZoekVakOpNaam("DataBase");
            if(vak2 ==null)  vak2 = new DbVak() { Naam = "DataBase", AantalLesuren = 80 };
            DbStudent dbStudent = ZoekStudent("Jan", "Jansens", new DateTime(1990, 1, 1));
            if (dbStudent == null)   
                 dbStudent = new DbStudent() { Voornaam = "Jan", Familienaam = "Jansens", 
                GeboorteDatum = new DateTime(1990, 1, 1) };
            //VoegStudentToeMetVakken(dbSchool, dbStudent,vak1,vak2); 
            Console.ReadKey();
        }

        private static DbStudent ZoekStudent(string voornaam, string familienaam, DateTime geboorteDatum)
        {
            using (SchoolDbContext db = new SchoolDbContext())
            {
                DbStudent dbStudent = db.DbStudenten.Where(s => s.Voornaam.ToLower() == voornaam.ToLower()
                                        && s.Familienaam.ToLower() == familienaam.ToLower()
                                        && s.GeboorteDatum == geboorteDatum).FirstOrDefault();
                return dbStudent;
            }
        }
        private static DbVak ZoekVakOpNaam(string vakNaam)
        {
            using (SchoolDbContext db = new SchoolDbContext())
            {
                DbVak dbVak = db.DbVakken.Where(param => param.Naam.ToLower() == vakNaam.ToLower()).FirstOrDefault();
                return dbVak;
            }
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
