namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int stutusCode, string message=null)
        {
            StutusCode = stutusCode;
            Message = message ?? GetDefaultMessageForStatusCode(StutusCode);
        }

        public int StutusCode { get; set; }
        public string Message { get; set; }

        private string GetDefaultMessageForStatusCode(int StutusCode)
        {
            return StutusCode switch
            {
                400 => "A bad request, you have made",
                401 => "Authorized, you are not",
                404 => "Resource found, it was not",
                500 => "Fear is the path to the dark side. Fear leads to anger. Anger leads to hate. Hate leads to career change",
                _ => null
            };
        }
    }
}
