namespace school_system_api.models
{
    public class Classroom : Base
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
        public string? Location { get; set; }
        public Teacher? Teacher { get; set; }
    }
}