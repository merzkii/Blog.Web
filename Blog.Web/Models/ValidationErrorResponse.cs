namespace Blog.Web.Models
{
    public class ValidationErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public List<ValidationError> Errors { get; set; }
    }

    public class ValidationError
    {
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
    }

}
