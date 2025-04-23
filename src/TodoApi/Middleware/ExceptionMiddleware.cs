using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace TodoApi.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
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
                // Log the exception with detailed information
                _logger.LogError(ex, "An unexpected error occurred");

                // Set the response status code and content type
                context.Response.ContentType = "application/json";

                ProblemDetails problemDetails = CreateProblemDetails(ex, context);

                // Return the error response in JSON format
                var json = JsonSerializer.Serialize(problemDetails);
                await context.Response.WriteAsync(json);
            }
        }

        private ProblemDetails CreateProblemDetails(Exception ex, HttpContext context)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var title = "An unexpected error occurred";
            var detail = _env.IsDevelopment() ? ex.StackTrace : "An unexpected error occurred";

            // Default ProblemDetails
            var problemDetails = new ProblemDetails
            {
                Title = title,
                Status = (int)statusCode,
                Detail = detail,
                Instance = context.Request.Path,
                Type = "https://httpstatuses.com/500"
            };

            // Handle specific exceptions
            if (ex is ValidationException)
            {
                statusCode = HttpStatusCode.BadRequest;
                title = "Validation error";
                detail = ex.Message;

                problemDetails.Status = (int)HttpStatusCode.BadRequest;
                problemDetails.Title = title;
                problemDetails.Detail = detail;
                problemDetails.Type = "https://httpstatuses.com/400";
            }
            else if (ex is UnauthorizedAccessException)
            {
                statusCode = HttpStatusCode.Unauthorized;
                title = "Unauthorized access";
                detail = "You are not authorized to access this resource.";

                problemDetails.Status = (int)HttpStatusCode.Unauthorized;
                problemDetails.Title = title;
                problemDetails.Detail = detail;
                problemDetails.Type = "https://httpstatuses.com/401";
            }
            else if (ex is KeyNotFoundException)
            {
                statusCode = HttpStatusCode.NotFound;
                title = "Resource not found";
                detail = "The requested resource could not be found.";

                problemDetails.Status = (int)HttpStatusCode.NotFound;
                problemDetails.Title = title;
                problemDetails.Detail = detail;
                problemDetails.Type = "https://httpstatuses.com/404";
            }
            else if (ex is ArgumentException)
            {
                statusCode = HttpStatusCode.BadRequest;
                title = "Invalid argument";
                detail = ex.Message;

                problemDetails.Status = (int)HttpStatusCode.BadRequest;
                problemDetails.Title = title;
                problemDetails.Detail = detail;
                problemDetails.Type = "https://httpstatuses.com/400";
            }
            else if (ex is InvalidOperationException)
            {
                statusCode = HttpStatusCode.BadRequest;
                title = "Invalid operation";
                detail = ex.Message;

                problemDetails.Status = (int)HttpStatusCode.BadRequest;
                problemDetails.Title = title;
                problemDetails.Detail = detail;
                problemDetails.Type = "https://httpstatuses.com/400";
            }
            else
            {
                // Generic server error
                problemDetails.Status = (int)HttpStatusCode.InternalServerError;
                problemDetails.Title = "Internal Server Error";
                problemDetails.Detail = _env.IsDevelopment() ? ex.StackTrace : "An unexpected error occurred.";
                problemDetails.Type = "https://httpstatuses.com/500";
            }

            // You can add more exception types here if needed (e.g., NotImplementedException, TimeoutException, etc.)

            return problemDetails;
        }
    }
}
