using school_system_api.models;

namespace school_system_api.Dto
{
    public class ClassroomDto : Base
    {
        public string? Name { get; set; }
        public int? Capacity { get; set; }
        public string? Location { get; set; }
        public int? TeacherId { get; set; }
    }

    public class AssignTeacherToClassroomDto
    {
        public int TeacherId { get; set; }
    }

}
