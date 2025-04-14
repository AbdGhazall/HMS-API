namespace HMS.API.Abstraction.Entities
{
    public class BaseResponseError
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;

        public DateTime MessageTime { get; set; }
    }
}