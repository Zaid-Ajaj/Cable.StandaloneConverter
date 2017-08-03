using System;

namespace ServerSide
{
    public enum Level
    {
        Beginner,
        Intermediate,
        Advanced
    }

    public class Student
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string[] Subjects { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Level Level { get; set; }
    }
}
