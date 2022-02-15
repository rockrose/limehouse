﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace LimehouseStudiosTechChallenge.Filters
{
    public class HandleException : ExceptionFilterAttribute
    {
        private readonly ILogger<HandleException> _logger;

        public HandleException(ILogger<HandleException> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception.Message);
            var result = new ViewResult { ViewName = "Error" };
            var modelMetadata = new EmptyModelMetadataProvider();
            result.ViewData = new ViewDataDictionary(modelMetadata, context.ModelState);
            result.ViewData.Add("HandleException", context.Exception);
            context.Result = result;
            context.ExceptionHandled = true;
        }
    }
}
