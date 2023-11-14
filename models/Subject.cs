namespace school_system_api.models
{
    public class Subject : Base
    {
        public string Name { get; set; }
        public ICollection<TeacherSubject>? TeacherSubjects { get; set; }
    }
}