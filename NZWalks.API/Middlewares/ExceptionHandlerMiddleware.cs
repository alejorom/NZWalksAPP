using System.Net;

namespace NZWalks.API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> logger;
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }

        /// <summary>
        /// Middleware para manejar excepciones no controladas en la aplicación.
        /// </summary>
        /// <param name="httpContext">El contexto HTTP actual.</param>
        /// <returns>Una tarea que representa la ejecución del middleware.</returns>
        /// <exception cref="Exception">Captura cualquier excepción no controlada.</exception>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var errorID = Guid.NewGuid().ToString();

                // Log the exception
                logger.LogError(ex, "{ErrorID} : {ErrorMessage}", errorID, ex.Message);

                // Create a custom error message response
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorID,
                    ErrorMessage = "Something went wrong! We are looking into resolving this."
                };

                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
