using Axon.Application.Common.Exceptions;
using Axon.Application.Common.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Axon.WebApi.Middleware
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

                IDictionary<string,string[]> errors = null;
                switch (error)
                {
                    case ValidationException e:
                        // custom Validation error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        errors = e.Failures;
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case AuthorizationException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                await response.WriteAsync(JsonSerializer.Serialize(ServiceResponse.Failure<Exception>(error.Message, errors)));
            }
        }
    }
}