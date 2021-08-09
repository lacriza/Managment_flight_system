using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Web.Middleware
{
  public class ErrorHandlerMiddleware
  {
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
      try
      {
        await _next(context);
      }
      catch (Exception error)
      {
        var response = context.Response;
        response.ContentType = "application/json";

        switch (error)
        {
          case ArgumentException e:
            // custom application error
            response.StatusCode = (int)HttpStatusCode.BadRequest;
            break;
          case KeyNotFoundException e:
            // not found error
            response.StatusCode = (int)HttpStatusCode.NotFound;
            break;
          default:
            // unhandled error
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            break;
        }

        var errors = new Dictionary<string, string>();
        errors.Add(error.ToString(), error?.Message);
        var result = JsonSerializer.Serialize(errors);
        await response.WriteAsync(result);
      }
    }
  }
}
