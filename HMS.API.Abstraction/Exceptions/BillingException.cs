namespace HMS.API.Abstraction.Exceptions
{
    public class BillingException : Exception
    {
        public int ErrorCode { get; set; }
        public int HttpStatusCode { get; set; }

        public BillingException(string message, int errorcode, int httpStatusCode) : base(message)
        {
            ErrorCode = errorcode;
            HttpStatusCode = httpStatusCode;
        }
    }
}