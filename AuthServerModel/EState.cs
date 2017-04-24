using System.ComponentModel.DataAnnotations;

namespace AuthServer.Domain
{

    public enum EState : int
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Display(Name = "正常")]
        Enable = 1,
        /// <summary>
        /// 禁用
        /// </summary>
        [Display(Name = "禁用")]
        Disable = 0,
        /// <summary>
        /// 已删
        /// </summary>
        [Display(Name = "已删")]
        Delete = 2,
    }
}
