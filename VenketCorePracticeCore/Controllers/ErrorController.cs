using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VenketCorePracticeCore.Controllers
{
    public class ErrorController:Controller
    {

        [Route("Error/{statusCode}")]

        public IActionResult HttpStatusCodeHandler(int statusCode)
        {

            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry! The Resource you are looking can't be found";
                    ViewBag.path = statusCodeResult.OriginalPath;
                    ViewBag.QS = statusCodeResult.OriginalQueryString;
                    ViewBag.baseUrl = statusCodeResult.OriginalPathBase;
                    break;
                default:
                    break;
            }

            return View("NotFount");
        }

        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionDetail = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            ViewBag.ExceptionPath = exceptionDetail.Path;

            ViewBag.ExceptionMessage = exceptionDetail.Error.Message;

            ViewBag.ExceptioStackTrace = exceptionDetail.Error.StackTrace;

            return View("Error");
        }
    }
}
