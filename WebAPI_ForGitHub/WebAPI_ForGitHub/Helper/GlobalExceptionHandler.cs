using NLog;
using System;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using System.Threading;
using System.Net;
using System.Web.Http.Results;
using System.Net.Http;

namespace WebAPI_ForGitHub.Helper
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            logger.Error(context.Exception, context.ExceptionContext.Exception.StackTrace);
            var response = context.Request.CreateResponse(HttpStatusCode.InternalServerError,
                new
                {
                    Message = "Oops!! An unexpected error occured"
                });

            context.Result = new ResponseMessageResult(response);
            return Task.FromResult<object>(response);
        }

    }
}