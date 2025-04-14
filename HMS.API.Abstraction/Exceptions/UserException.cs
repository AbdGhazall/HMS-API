namespace HMS.API.Abstraction.Exceptions
{
    public class UserException : Exception
    {
        public int ErrorCode { get; set; }
        public int HttpStatusCode { get; set; }

        public UserException(string message, int errorcode, int httpStatusCode) : base(message)
        {
            ErrorCode = errorcode;
            HttpStatusCode = httpStatusCode;
        }
    }
}