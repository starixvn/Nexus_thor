namespace Thor.WebAPI
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Primitives;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ThorAuthAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private const string accessToken = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJvcGVyYXRvcl9jb2RlIjoiT1AwMDAwNyIsIm5hbWUiOiJIdUdIIFpPTkUgQ0FQSVRBTCBJTlZFU1RNgU5UIEdST1VQIExJTUlU88QifQ.iymrRsZZ_O9NCKIaxUIYN3rU2c8w0zh70to3GcPwW8";
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            StringValues authorizationHeader;
            context.HttpContext.Request.Headers.TryGetValue("Authorization", out authorizationHeader);
            if (!authorizationHeader.ToString().Equals(accessToken))
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
