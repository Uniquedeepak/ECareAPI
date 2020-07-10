using ECare.API.Services;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECare.API.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(string ConnectionstringName)
            : base(ConnectionstringName, throwIfV1Schema: false)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public static ApplicationDbContext Create()
        {
            string CsName = string.IsNullOrEmpty(ConnectionStringNames.DBIdentityName) ? "DefaultConnection" : ConnectionStringNames.DBIdentityName;
            return new ApplicationDbContext(CsName);
        }

    }
}