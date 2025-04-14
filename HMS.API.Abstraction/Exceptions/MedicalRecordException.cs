namespace HMS.API.Abstraction.Exceptions
{
    public class MedicalRecordException : Exception
    {
        public int ErrorCode { get; set; }
        public int HttpStatusCode { get; set; }

        public MedicalRecordException(string message, int errorcode, int httpStatusCode) : base(message)
        {
            ErrorCode = errorcode;
            HttpStatusCode = httpStatusCode;
        }
    }
}