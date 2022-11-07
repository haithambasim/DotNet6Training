namespace Training.Data.Dtos
{
    public class UserLoginResultDto
    {
        public UserAccountDto Account { get; set; }
        public string Token { get; set; }
        public DateTime Expiry { get; set; }
    }
}
