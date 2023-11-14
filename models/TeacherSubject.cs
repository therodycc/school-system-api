namespace school_system_api.models
{
    public class TeacherSubject : Base
    {
        public int TeacherId { get; set; }
        public int SubjectId { get; set; }
        public Teacher Teacher { get; set; }
        public Subject Subject { get; set; }
    }
}