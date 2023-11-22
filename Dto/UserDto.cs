using school_system_api.models;

namespace school_system_api.Dto
{
    public class UserDto : Base
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool? Active { get; set; }
    }

    public class CreateUserDto : Base
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool? Active { get; set; }
        public string Password { get; set; }
    }
}
