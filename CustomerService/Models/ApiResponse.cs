namespace CustomerService.Models
{
    public class ApiResponse
    {
        public object? Result { get; set; }
        public string[] Errors { get; set; }

        public ApiResponse(object result, string[] errors)
        {
            Result = result;
            Errors = errors;
        }
        public ApiResponse()
        {
            Result = null;
            Errors = Array.Empty<string>();
        }
    }
}
