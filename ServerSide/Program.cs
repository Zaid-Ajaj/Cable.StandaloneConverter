using System;
using Nancy;
using Nancy.Hosting.Self;
using Cable.Nancy;
using Cable;
using System.IO;

namespace ServerSide
{
    public class StudentServiceModule : NancyModule
    {
        public StudentServiceModule()
        {
            Get["/get-best-student"] = args =>
            {
                var bestStudent = new Student
                {
                    Id = 1,
                    Level = Level.Advanced,
                    Name = "Albert",
                    Subjects = new string[] { "Physics" },
                    DateOfBirth = DateTime.Now.AddYears(-25)
                };

                return Json.Serialize(bestStudent);
            };


            Post["/echo-student-subjects", runAsync: true] = async (args, ct) =>
            {
                using (var streamReader = new StreamReader(Request.Body))
                {
                    var requestBodyContents = await streamReader.ReadToEndAsync();
                    var student = Json.Deserialize<Student>(requestBodyContents);
                    var subjects = student.Subjects;
                    return Json.Serialize(subjects);
                }
            };
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new NancyHost(new Uri("http://localhost:8080")))
            {
                host.Start();
                Console.WriteLine("Server started at http://localhost:8080");
                Console.WriteLine("Press any key to exit...");
                Console.ReadLine();
            }
        }
    }
}
