using ECare.API.Services;
using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web;

namespace ECare.API.Models
{
    public class SchoolDBHandler : DelegatingHandler
    {
        protected override async System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            if (true)
            {
                SetIdentityConnectionString(HttpContext.Current.User); 
            }

            var response = await base.SendAsync(request, cancellationToken);
            return response;
        }

        private static void SetIdentityConnectionString(IPrincipal principal)
        {
            ConnectionStringNames obj = new ConnectionStringNames();
            var claimIdentity = principal as ClaimsPrincipal;

            if (claimIdentity.HasClaim(x => x.Type == "SchoolCode"))
            {
                string schoolCode = claimIdentity.Claims.Where(x => x.Type.Equals("SchoolCode")).FirstOrDefault().Value;
                obj.GetConnectionStringName(schoolCode);
            }
        }

    }

}