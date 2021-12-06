namespace API.Errors
{
    public class ApiException : ApiResponse
    {
        public ApiException(int stutusCode, string message = null, string details = null)
            : base(stutusCode, message)
        {
            Details = details;
        }

        public string Details { get; set; }
    }
}
