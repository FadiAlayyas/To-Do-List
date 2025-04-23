using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace TodoApi.Helpers
{
    public class ApiResponser
    {
        // Success Response
        public static IActionResult Success(object data = null, string message = "Request was successful", int statusCode = (int)HttpStatusCode.OK)
        {
            return new JsonResult(new ApiResponse
            {
                Success = true,
                Message = message,
                Data = data,
                StatusCode = statusCode
            })
            {
                StatusCode = statusCode
            };
        }

        // Error Response
        public static IActionResult Error(string message, int statusCode = (int)HttpStatusCode.InternalServerError, object errors = null)
        {
            return new JsonResult(new ApiResponse
            {
                Success = false,
                Message = message,
                Data = errors,
                StatusCode = statusCode
            })
            {
                StatusCode = statusCode
            };
        }

        // Not Found Response
        public static IActionResult NotFound(string message = "Resource not found")
        {
            return Error(message, (int)HttpStatusCode.NotFound);
        }

        // BadRequest Response
        public static IActionResult BadRequest(string message = "Bad request", object errors = null)
        {
            return Error(message, (int)HttpStatusCode.BadRequest, errors);
        }

        // Unauthorized Response
        public static IActionResult Unauthorized(string message = "Unauthorized access")
        {
            return Error(message, (int)HttpStatusCode.Unauthorized);
        }
    }

    // Response Structure
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public int StatusCode { get; set; }
    }
}
