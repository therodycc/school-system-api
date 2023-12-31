namespace school_system_api.models
{
    public class Teacher : Base
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Profession { get; set; }
        public string Email { get; set; }
        public ICollection<Classroom>? Classrooms { get; set; }
        public ICollection<TeacherSubject>? TeacherSubjects { get; set; }
    }
}