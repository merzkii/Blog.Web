using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.DTO.Error
{
    public class ApiErrorResponseDTO
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string? Details { get; set; }
        public List<ValidationErrorItem>? Errors { get; set; }

        public ApiErrorResponseDTO(int statusCode, string message, string? details = null)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }
    }

    public class ValidationErrorItem
    {
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
    }
}
