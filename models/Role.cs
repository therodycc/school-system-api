using school_system_api.Enum;

namespace school_system_api.models
{
    public class Role : Base
    {
        public RoleTypes Name { get; set; }

        public ICollection<User>? Users { get; set; }
    }
}