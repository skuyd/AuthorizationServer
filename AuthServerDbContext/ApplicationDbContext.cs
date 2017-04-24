
using AuthServer.Domain;
using AuthServer.Domain.Sys;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServerDbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, RoleEntity, string, IdentityUserLogin, MUserRole, IdentityUserClaim>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
        /// <summary>
        /// 客户端服务器
        /// </summary>
        public DbSet<ClientEntity> ClientEntitys { get; set; }

        /// <summary>
        /// 客户端服务器
        /// </summary>
        public DbSet<RoleEntity> RoleEntitys { get; set; }
        /// <summary>
        /// 客户端服务器
        /// </summary>
        public DbSet<MUserClient> UserClients { get; set; }
        /// <summary>
        /// 客户端服务器
        /// </summary>
        public DbSet<MUserRole> UserRoles { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }
}
