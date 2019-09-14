using System;
using System.Collections.Generic;
using System.Net;
using Exterminator.Models;
using Exterminator.Models.Exceptions;
using Exterminator.Repositories.Implementations;
using Exterminator.Services.Implementations;
using Exterminator.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Exterminator.WebApi.ExceptionHandlerExtensions
{
    public static class ExceptionHandlerExtensions
    {
        public static void UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler( 
                error =>
                {
                    error.Run(async context =>
                    {
                        // Retrieve the exception handler feature
                        var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                        if (exceptionHandlerFeature != null)
                        {
                            var exception = exceptionHandlerFeature.Error;
                            var statusCode = (int) HttpStatusCode.InternalServerError;
                            context.Response.ContentType = "application/json";

                            if(exception is ResourceNotFoundException)
                                statusCode = (int) HttpStatusCode.NotFound;
                            else if (exception is ModelFormatException)
                                statusCode = (int) HttpStatusCode.PreconditionFailed;
                            else if (exception is ArgumentOutOfRangeException)
                                statusCode = (int) HttpStatusCode.BadRequest;
                            

                            ExceptionModel model = new ExceptionModel
                                                    {
                                                        StatusCode = statusCode,
                                                        ExceptionMessage = exception.Message,
                                                        StackTrace = exception.StackTrace
                                                    };
                            context.Response.StatusCode = statusCode;
                            LogService log = new LogService(new LogRepository());
                            log.LogToDatabase(model);
                            await context.Response.WriteAsync(model.ToString());
                        }
                    });
                }
            );
        }
    }
}