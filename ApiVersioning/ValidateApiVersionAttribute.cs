using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiVersioning
{
    public class ValidateApiVersionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var routeData = context.HttpContext.GetRouteData();
            var versionInPath = routeData.Values["version"]?.ToString();
            var versionInQuery = context.HttpContext.Request.Query["api-version"].ToString();
            versionInPath = NormalizeVersion(versionInPath!);
            versionInQuery = NormalizeVersion(versionInQuery);
            if (!string.IsNullOrEmpty(versionInQuery))
            {
                if (versionInPath != versionInQuery)
                {
                    context.Result = new BadRequestObjectResult("Query and path versions don't match");
                    return;
                }
            }

            base.OnActionExecuting(context);
        }
        private string NormalizeVersion(string version)
        {
            if (string.IsNullOrEmpty(version))
                return version;
            return version.TrimEnd('.', '0');
        }

    }
}
