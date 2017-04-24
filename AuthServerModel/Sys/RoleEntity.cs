using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace AuthServer.Domain.Sys
{
    public class RoleEntity : IdentityRole, IBaseEntity
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
    }
}
