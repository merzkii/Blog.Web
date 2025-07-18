using Blog.Application.DTO.Error;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace Blog.API.Filters
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.ContentType = "application/json";

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };


                if (ex is FluentValidation.ValidationException validationException)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;

                    var validationErrors = validationException.Errors
                        .Select(error => new ValidationErrorItem
                        {
                            PropertyName = error.PropertyName,
                            ErrorMessage = error.ErrorMessage
                        })
                        .ToList();

                    var validationResponse = new ApiErrorResponseDTO(
                        StatusCodes.Status400BadRequest,
                        "Validation failed"
                    )
                    {
                        Errors = validationErrors
                    };

                    await context.Response.WriteAsync(JsonSerializer.Serialize(validationResponse, options));
                    return;
                }


                context.Response.StatusCode = ex switch
                {
                    Blog.Application.Exceptions.NotFoundException => StatusCodes.Status404NotFound,
                    Blog.Application.Exceptions.BadRequestException => StatusCodes.Status400BadRequest, 
                    UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                    _ => StatusCodes.Status500InternalServerError
                };

                var errorResponse = _env.IsDevelopment()
     ? new ApiErrorResponseDTO(context.Response.StatusCode, ex.Message, ex.StackTrace)
     : new ApiErrorResponseDTO(context.Response.StatusCode, ex.Message);

                await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse, options));
            }
        }

    }
}
