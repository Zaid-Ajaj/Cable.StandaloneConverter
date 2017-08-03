using System;
using Cable.Bridge;
using ServerSide;

namespace ClientSide
{
    
    public class Program
    {

        public static async void Main()
        {
            // call the GET handler on the server
            var bestStudentJson = await Http.GetAsync("/get-best-student");
            var bestStudent = Json.Deserialize<Student>(bestStudentJson);

            Console.WriteLine($"Student: {bestStudent.Name}");
            Console.WriteLine($"Level: {bestStudent.Level}");
            Console.WriteLine($"Birthday: {bestStudent.DateOfBirth.ToString("dd/MM/yyyy")}");


            // Call the POST handler on the server
            var echoedStudentSubjects = await Http.PostAsync("/echo-student-subjects", bestStudentJson);
            var studentSubjects = Json.Deserialize<string[]>(echoedStudentSubjects);
            foreach(var subject in studentSubjects)
            {
                Console.WriteLine(subject);
            }
        }
    }
}