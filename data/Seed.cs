using school_system_api.data;
using school_system_api.models;
using school_system_api.Enum;

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
            SeedStudents();
            SeedClassrooms();
            SeedRoles();
            SeedUsers();
            SeedTeacherSubject();
            dataContext.SaveChanges();
        }

        private void SeedTeacherSubject()
        {
            if (!dataContext.TeacherSubjects.Any())
            {
                var teacherSubjects = new List<TeacherSubject>()
                {
                    new TeacherSubject() {
                        Teacher = new Teacher() {  Name = "Jane", LastName = "Smith", Profession = "Educación", Email = "jane.smith@example.com" },
                        Subject = new Subject() {  Name = "Mathematics" },
                    },
                    new TeacherSubject {
                        Teacher = new Teacher() {  Name = "Marcus", LastName = "Doe", Profession = "Educación", Email = "Marcus.doe@example.com", },
                        Subject = new Subject() {  Name = "Physical"},
                    },
                    new TeacherSubject {
                        Teacher = new Teacher() {  Name = "Jenny", LastName = "Gonzalez", Profession = "Educación", Email = "jennygonzalez@example.com", },
                        Subject = new Subject() {  Name = "English"},
                    },
                };

                dataContext.TeacherSubjects.AddRange(teacherSubjects);
            }
        }

        private void SeedStudents()
        {
            if (!dataContext.Students.Any())
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
        }

        private void SeedClassrooms()
        {
            if (!dataContext.Classrooms.Any())
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
        private void SeedRoles()
        {
            if (!dataContext.Roles.Any())
            {
                var roles = new List<Role>
            {
                new Role
                {
                    Name = RoleTypes.Admin
                },
                new Role
                {
                    Name = RoleTypes.User
                },
            };

                dataContext.Roles.AddRange(roles);
            }
        }
        private void SeedUsers()
        {
            if (!dataContext.Users.Any())
            {
                var users = new List<User>
            {
                new User
                {
                    FirstName = "Admin",
                    LastName = "Test",
                    Active = true,
                    Email = "admin@example.com",
                    Password = "$2b$10$lX4lIF5VysCR8emGBjn9qOc98ftL2iqN4xPy.KGzVH3WY27HMcsv.",
                    RoleId = 1
                },
            };

                dataContext.Users.AddRange(users);
            }
        }
    }


}
