using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthServer.Domain.Sys
{
    public class ApplicationUser : IdentityUser, IBaseEntity
    {
        /// <summary>
        /// 创建人Id
        /// </summary>
        public virtual string CreatorId
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人
        /// </summary>
        public virtual string CreatorTrueName
        {
            get;
            set;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 删除标志位
        /// </summary> 
        public virtual EState State
        {
            get;
            set;
        }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public virtual string TrueName { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // 请注意，authenticationType 必须与 CookieAuthenticationOptions.AuthenticationType 中定义的相应项匹配
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // 在此处添加自定义用户声明
            return userIdentity;
        }
    }
}
