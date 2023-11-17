using school_system_api.models;

namespace school_system_api.Dto
{
    public class TeacherDto : Base
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Profession { get; set; }
        public string Email { get; set; }
    }

    public class SubjectsToTeacherDto : Base
    {
        public int SubjectId { get; set; }
    }
}
