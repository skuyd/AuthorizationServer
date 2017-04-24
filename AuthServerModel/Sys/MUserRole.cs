using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AuthServer.Domain.Sys
{
    /// <summary>
    /// 用户角色
    /// </summary>
    public class MUserRole : IdentityUserRole<string>
    {
    }
}
