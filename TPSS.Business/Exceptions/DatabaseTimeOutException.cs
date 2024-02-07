using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Newtonsoft.Json;
using TPSS.Business.Common;
using TPSS.Business.Exceptions;
using TPSS.Data.Models.DTO;

namespace TPSS.Business.Exceptions
{

    public sealed class DatabaseTimeOutException : IExceptionHandler
    {
        private readonly ILogger<DatabaseTimeOutException> _logger;

        public DatabaseTimeOutException(ILogger<DatabaseTimeOutException> logger)
        {
            _logger = logger;
        }

        public Exception HandleException(Exception exception, Guid handlingInstanceId)
        {
            throw new NotImplementedException();
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(
                exception, "Exception occurred: {Message}", exception.Message);

            var problemDetails = new ExceptionHandlerResponse
            {
                isSuccess = false,
                isFailure = true,
                ErrorDetail = new ErrorResponse
                {
                    code = StatusCodes.Status400BadRequest,
                    description = exception.Message
                }
            };

            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            var errorMessage = JsonConvert.SerializeObject(problemDetails);

            await httpContext.Response.WriteAsync(errorMessage, cancellationToken);

            return true;
        }
    }
}

