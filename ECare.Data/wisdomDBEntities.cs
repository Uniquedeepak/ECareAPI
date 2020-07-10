using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ECare.Data
{
    public partial class wisdomDBEntities : DbContext
    {
        public wisdomDBEntities(string ConnectionName)
            : base(ConnectionName)
        {
        }
    }
}