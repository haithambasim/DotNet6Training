namespace Training.Exceptions
{
    public class UserFriendlyException : Exception
    {
        public string Message { get; set; }
        public UserFriendlyException(string message)
        {
            Message = message;
        }
    }
}
