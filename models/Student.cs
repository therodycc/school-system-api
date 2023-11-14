namespace school_system_api.models
{
    public class Student : Base
    {
        public string Name { get; set; }
        public string? LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string? StudentCode { get; set; }
        public string? MotherName { get; set; }
        public string? FatherName { get; set; }
        public string? Address { get; set; }
    }
}