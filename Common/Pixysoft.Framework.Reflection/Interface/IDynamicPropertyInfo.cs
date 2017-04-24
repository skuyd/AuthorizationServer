using System;
using System.Reflection;

namespace Pixysoft.Framework.Reflection
{
    /// <summary>
    /// 动态属性
    /// </summary>
    public interface IDynamicPropertyInfo
    {
        /// <summary>
        /// 属性名称
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// 属性数据类型
        /// </summary>
        Type PropertyType
        {
            get;
        }

        /// <summary>
        /// 原属性
        /// </summary>
        PropertyInfo Info
        {
            get;
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        object GetValue(object obj, object[] index);

        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        /// <param name="index"></param>
        void SetValue(object obj, object value, object[] index);
    }
}