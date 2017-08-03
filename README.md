# Cable.StandaloneConverter
The [Cable](https://github.com/Zaid-Ajaj/Cable) converter can be used standalone without HTTP abstractions. This project demonstrates that:

### Using Cable converter on the server with GET handler:
```cs
// server
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
```
then using this little [Http](https://github.com/Zaid-Ajaj/Cable.StandaloneConverter/blob/master/ClientSide/Http.cs) class helper you can `GET` the json from server and deserialize it on the client to a concrete object:
```cs
// call the GET handler on the server from client
var bestStudentJson = await Http.GetAsync("/get-best-student");
var bestStudent = Json.Deserialize<Student>(bestStudentJson);

Console.WriteLine($"Student: {bestStudent.Name}"); // logs "Student: Albert"
Console.WriteLine($"Level: {bestStudent.Level}"); // logs "Level: Advanced"
Console.WriteLine($"Birthday: {bestStudent.DateOfBirth.ToString("dd/MM/yyyy")}"); // logs "Birthday: 03/08/1992"
```
### Using Cable on the server with POST handler
here we deserialize the incoming JSON to a concrete object on the server and return (echo) the subjects back:
```cs
// server
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
```
then from the client you can post the data:
```cs
// Call the POST handler on the server
var echoedStudentSubjects = await Http.PostAsync("/echo-student-subjects", bestStudentJson);
var studentSubjects = Json.Deserialize<string[]>(echoedStudentSubjects);
foreach(var subject in studentSubjects)
{
    Console.WriteLine(subject);
}
```

# Build the solution
- Clone the repo and open the solution.
- Build using `Ctrl + Shift + B`. 
- Run the `ServerSide` project 
- Navigate to http://localhost:8080
