using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace WebAPI_ForGitHub.Helper
{
    public class GLobalExceptionLogger : ExceptionLogger
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public override void Log(ExceptionLoggerContext context)
        {
            var log = context.Exception.ToString();
            Trace.TraceError(context.ExceptionContext.Exception.ToString());
            //Write the exception to your logs
            logger.Error(context.Exception, "Oops, something unexpected happened!!");
        }
    }
}