namespace Week6.Club.DataDomain.Migrations.Week6DbContextMigrations
{
    using CsvHelper;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<Week6.Club.DataDomain.Week6DbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\Week6DbContextMigrations";
        }

        protected override void Seed(Week6.Club.DataDomain.Week6DbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var students = new List<StudentData>();
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = "Week6.Club.DataDomain.StudentList1.csv";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    CsvReader csvReader = new CsvReader(reader);
                    csvReader.Configuration.HasHeaderRecord = false;
                    students = csvReader.GetRecords<StudentData>().ToList();
                    foreach (var student in students)
                    {
                        context.Students.AddOrUpdate(new Students
                        {
                            StudentNumber = student.StudentNumber,
                            Firstname = student.Firstname,
                            Surname = student.Surname
                        });
                    }

                }
            }
            context.SaveChanges();

            context.Clubs.AddOrUpdate(new Clubs[]
            { new Clubs{ClubName = "The Chess Club", CreationDate=DateTime.Parse("25/01/2017")}
            });

            context.SaveChanges();

            var studentList = (from s in context.Students
                               select new
                               {
                                  s.StudentNumber
                               }).OrderBy(s => s.StudentNumber).Take(10).ToList();

            var studentList2 = (from s in context.Students
                               select s).OrderBy(s => s.StudentNumber).Take(10).ToList();

            foreach (var student in studentList2) {

                context.Clubs.First().clubMembers.Add(new Members
                {
                    studentMember = student,
                    StudentNumber = student.StudentNumber,
                    approved = false
                });
            }

            context.Clubs.First().clubMembers.First().approved = true;
            context.Clubs.First().adminID = context.Clubs.First().clubMembers.First().MemberID;

        }
    }
}
