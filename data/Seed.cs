using school_system_api.data;
using school_system_api.models;

namespace school_system_api
{
    public class Seed
    {
        private readonly DataContext dataContext;

        public Seed(DataContext context)
        {
            this.dataContext = context;
        }

        public void SeedDataContext()
        {
            if (!dataContext.TeacherSubjects.Any())
            {
                SeedStudents();
                SeedClassrooms();
                var teacherSubjects = new List<TeacherSubject>()
                {
                    new TeacherSubject() {
                        Teacher = new Teacher() {  Name = "Jane", LastName = "Smith", Profession = "English Teacher", Email = "jane.smith@example.com" },
                        Subject = new Subject() {  Name = "Mathematics" },
                    },
                    new TeacherSubject {
                        Teacher = new Teacher() {  Name = "Marcus", LastName = "Doe", Profession = "Educación", Email = "Marcus.doe@example.com", },
                        Subject = new Subject() {  Name = "English"},
                    },
                };

                dataContext.TeacherSubjects.AddRange(teacherSubjects);
                dataContext.SaveChanges();
            }
        }

        private void SeedStudents()
        {
            var students = new List<Student>
            {
                new Student
                {
                    Name = "Alice",
                    LastName = "Johnson",
                    Birthday = new DateTime(2005, 5, 15),
                    StudentCode = "A12345",
                    MotherName = "Mary Johnson",
                    FatherName = "John Johnson",
                    Address = "123 Main St",
                },
                new Student
                {
                    Name = "Marcs",
                    LastName = "Perez",
                    Birthday = new DateTime(2000, 5, 15),
                    StudentCode = "A000000",
                    MotherName = "Martha Perez",
                    FatherName = "Ronny Perez",
                    Address = "503 King St",
                },
            };
            dataContext.Students.AddRange(students);
        }

        private void SeedClassrooms()
        {
            var classrooms = new List<Classroom>
            {
                new Classroom
                {
                    Name = "Class A",
                    Capacity = 30,
                    Location = "Room 101",
                },
                new Classroom
                {
                    Name = "Class B",
                    Capacity = 40,
                    Location = "Room 102",
                },
            };

            dataContext.Classrooms.AddRange(classrooms);
        }
    }


}
