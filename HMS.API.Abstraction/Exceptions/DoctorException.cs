namespace HMS.API.Abstraction.Exceptions
{
    public class DoctorException : Exception
    {
        public int ErrorCode { get; set; }
        public int HttpStatusCode { get; set; }

        public DoctorException(string message, int errorcode, int httpStatusCode) : base(message)
        {
            ErrorCode = errorcode;
            HttpStatusCode = httpStatusCode;
        }
    }
}