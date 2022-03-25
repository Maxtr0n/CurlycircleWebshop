using BLL.Exceptions;
using BLL.ViewModels;
using Domain.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CurlycircleWebApi.Infrastructure
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IWebHostEnvironment env;
        private readonly ILogger<ErrorHandlerMiddleware> logger;

        public ErrorHandlerMiddleware(
            RequestDelegate next,
            IWebHostEnvironment env,
            ILogger<ErrorHandlerMiddleware> logger)
        {
            this.next = next;
            this.env = env;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                // Call the next middleware in the pipeline
                await next(httpContext);
            }
            catch (EntityNotFoundException e)
            {
                logger.LogError(e, "Unhandled exception caught.");
                await SendResponseAsync(
                    httpContext,
                    (int)HttpStatusCode.NotFound,
                    new ErrorViewModel
                    {
                        Message = e.Message,
                        Stacktrace = e.StackTrace
                    });
            }
            catch (DomainException e)
            {
                logger.LogError(e, "Unhandled exception caught.");
                await SendResponseAsync(
                    httpContext,
                    (int)HttpStatusCode.BadRequest,
                    new ErrorViewModel
                    {
                        Message = e.Message,
                        Stacktrace = e.StackTrace
                    });
            }
            catch (ValidationAppException e)
            {
                logger.LogError(e, "Unhandled exception caught.");
                httpContext.Response.Clear();
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                httpContext.Response.ContentType = "application/json";

                var json = SerializeObject(new
                {
                    e.Message,
                    e.Errors
                });
                await httpContext.Response.WriteAsync(json);
            }
            catch (AppException e)
            {
                logger.LogError(e, "Unhandled exception caught.");
                await SendResponseAsync(
                    httpContext,
                    (int)HttpStatusCode.BadRequest,
                    new ErrorViewModel
                    {
                        Message = e.Message,
                        Stacktrace = e.StackTrace
                    });
            }
            catch (NoAccessException e)
            {
                await SendResponseAsync(
                    httpContext,
                    (int)HttpStatusCode.Forbidden,
                    new ErrorViewModel
                    {
                        Message = e.Message,
                        Stacktrace = e.StackTrace
                    });
            }
            catch (Exception e)
            {
                logger.LogError(e, "Unhandled exception caught.");
                await SendResponseAsync(
                    httpContext,
                    (int)HttpStatusCode.InternalServerError,
                    new ErrorViewModel
                    {
                        Message = "An unexpected error occured.",
                        Stacktrace = e.StackTrace
                    });
            }
        }

        private async Task SendResponseAsync(HttpContext httpContext, int statusCode, ErrorViewModel errorDto)
        {
            httpContext.Response.Clear();
            httpContext.Response.StatusCode = statusCode;
            httpContext.Response.ContentType = "application/json";

            var json = env.IsDevelopment()
                ? SerializeObject(errorDto)
                : string.IsNullOrEmpty(errorDto.Message)
                    ? SerializeObject("")
                    : SerializeObject(errorDto.Message);

            await httpContext.Response.WriteAsync(json);
        }

        private string SerializeObject(object obj)
        {
            return JsonConvert.SerializeObject(
                obj,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
        }
    }
}
