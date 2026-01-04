using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using SBEISK.SGM.Domain.Exceptions;
using SBEISK.SGM.Infraestructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SBEISK.SGM.Presentation.API.Middlewares
{
    public class GlobalMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalMiddleware> _logger;

        public GlobalMiddleware(ILogger<GlobalMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                IList<Claim> instalations = context.Request.HttpContext.User.Claims.Where(x => x.Type == "Installation").ToList();
                var isAdmin = context.Request.HttpContext.User.Claims.Where(x => x.Type == "admin").ToList();

                var db = (SgmDataContext)context.RequestServices.GetService(typeof(SgmDataContext));
                
                if (context.Request.HttpContext.User.Identity.IsAuthenticated)
                {
                    int userId = context.Request.HttpContext.User.Claims.Where(x => x.Type == "UserId").Select(x => int.Parse(x.Value)).First();
                    db.SetUserId(userId);
                }

                if (context.Request.Headers.TryGetValue("Installation", out StringValues value) && value.Count > 0 && int.TryParse(value.GetEnumerator().Current, out int instalationId))
                {
                    
                    if (!instalations.Any(x => x.Value == instalationId.ToString()))
                    {
                        await HandleUnauthorizedAsync(context);
                        return;
                    }
                    db.SetInstalationId(instalationId);
                }

                await next(context);

                if (db.ChangeTracker.HasChanges())
                {
                    await db.SaveChangesAsync();
                }
            }
            catch (EntityBadRequestException ex)
            {
                await Handle400ResultAsync(context, ex);
            }
            catch (EntityCannotBeDeletedException ex)
            {
                await Handle400ResultAsync(context, ex);
            }
            catch (EntityNotFoundException ex)
            {
                await Handle404ResultAsync(context, ex);
            }
            catch (InvalidOperationException ex)
            {
                await HandleBadRequestResultAsync(context, ex);
            }
            catch( UnauthorizedAccessException )
            {
                await HandleUnauthorizedAsync(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unexpected error: {ex}");
                await HandleExceptionAsync(context);
            }
        }

        private Task WriteResponseAsync(HttpContext context, HttpStatusCode statusCode, object response)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }

        private Task HandleBadRequestResultAsync(HttpContext context, InvalidOperationException ex)
        {
            Response json = new Response(HttpStatusCode.BadRequest, ex.Message);
            return WriteResponseAsync(context, HttpStatusCode.BadRequest, json);
        }

        private Task HandleExceptionAsync(HttpContext context)
        {
            Response json = new Response(HttpStatusCode.InternalServerError, "An error occurred while processing your request");
            return WriteResponseAsync(context, HttpStatusCode.InternalServerError, json);
        }

        private Task HandleUnauthorizedAsync(HttpContext context)
        {
            Response json = new Response(HttpStatusCode.Unauthorized, "Unauthorized");
            return WriteResponseAsync(context, HttpStatusCode.Unauthorized, json);
        }

        private Task Handle400ResultAsync(HttpContext context, Exception ex)
        {
            Response json = new Response(HttpStatusCode.BadRequest, ex.Message);
            return WriteResponseAsync(context, HttpStatusCode.BadRequest, json);
        }
        private Task Handle404ResultAsync(HttpContext context, Exception ex)
        {
            Response json = new Response(HttpStatusCode.NotFound, ex.Message);
            return WriteResponseAsync(context, HttpStatusCode.NotFound, json);
        }

    }

    class Response
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public Response(HttpStatusCode statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}
