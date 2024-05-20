using EvoHub.Domain;
using EvoHub.Infra.CrossCutting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;

namespace EvoHub.Infra.Middlewares
{
    public class GlobalExceptionHandling
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandling(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new ActionResult<object>()
            {
                IsValid = false,
                Message = Constants.GetErrorMessage(exception),
                Result = exception.Data,
            };

            var jsonResponse = JsonConvert.SerializeObject(response, new JsonSerializerSettings
            {
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
            });

            return context.Response.WriteAsync(jsonResponse);
        }
    }
}
