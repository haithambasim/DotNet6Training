using Training.Data.Entities.Shared;

namespace Training.Data.Entities
{
    public class UserAccount : Entity<long>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
    }
}
