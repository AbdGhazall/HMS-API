namespace HMS.API.Abstraction.Exceptions
{
    public class AppointmentException : Exception
    {
        public int ErrorCode { get; set; }
        public int HttpStatusCode { get; set; }

        public AppointmentException(string message, int errorcode, int httpStatusCode) : base(message)
        {
            ErrorCode = errorcode;
            HttpStatusCode = httpStatusCode;
        }
    }
}