namespace school_system_api.models
{
    public class User : Base
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool? Active { get; set; }
    }
}