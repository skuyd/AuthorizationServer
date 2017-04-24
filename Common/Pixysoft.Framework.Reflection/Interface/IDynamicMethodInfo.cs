using System.Reflection;

namespace Pixysoft.Framework.Reflection
{
    /// <summary>
    /// 动态方法
    /// </summary>
    public interface IDynamicMethodInfo
    {
        /// <summary>
        /// 原属性
        /// </summary>
        MethodInfo Info
        {
            get;
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        object Invoke(object obj, object[] parameters);
    }
}