using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SportsStore.Web.Filters
{
    public class SimpleCacheAttribute : Attribute, IResourceFilter
    {
        private readonly Dictionary<PathString, IActionResult> _cachedResponses = new Dictionary<PathString, IActionResult>();
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            PathString path = context.HttpContext.Request.Path;

            if (_cachedResponses.ContainsKey(path))
            {
                context.Result = _cachedResponses[path];
                _cachedResponses.Remove(path);
            }
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            _cachedResponses.Add(context.HttpContext.Request.Path, context.Result);
        }
    }
}
