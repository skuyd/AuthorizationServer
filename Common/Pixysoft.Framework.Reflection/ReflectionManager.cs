using System;
using System.Reflection;

namespace Pixysoft.Framework.Reflection
{
    /// <summary>
    /// 
    /// </summary>
    public class ReflectionManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IDynamicType CreateDynamicType(Type type)
        {
            return new DynamicType(type);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static IDynamicPropertyInfo CreateDynamicProperty(PropertyInfo info)
        {
            return new DynamicPropertyInfo(info.DeclaringType, info);
        }
    }
}