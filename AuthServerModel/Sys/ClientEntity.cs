using System;

namespace AuthServer.Domain.Sys
{
    /// <summary>
    /// 客户端服务器
    /// </summary>
    public class ClientEntity : BaseEntity
    {
        /// <summary>
        /// 域名前缀
        /// </summary>
        public virtual string DomainNamePrefix { get; set; }

        /// <summary>
        /// 客户端所属组织名称
        /// </summary>
        public virtual string OrgernazeName { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string Address { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// 最大用户数
        /// </summary>
        public virtual int MaxUsers { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public virtual DateTime ValidityTerm { get; set; }
    }
}
