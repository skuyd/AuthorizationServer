using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Domain
{
    public interface IBaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        string Id
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人Id
        /// </summary>
        string CreatorId
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人
        /// </summary>
        string CreatorTrueName
        {
            get;
            set;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 删除标志位
        /// </summary> 
        EState State
        {
            get;
            set;
        }
    }
}
