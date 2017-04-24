using System;
using System.Reflection;
using Pixysoft.Framework.Reflection.Controller;

namespace Pixysoft.Framework.Reflection
{
    internal delegate object DynamicConstructorInfoHandler(object[] parameters);

    /// <summary>
    /// 
    /// </summary>
    public class DynamicConstructorInfo : IDynamicConstructorInfo
    {
        private Type type = null;
        private ConstructorInfo info;
        private DynamicConstructorInfoHandler handler = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="info"></param>
        public DynamicConstructorInfo(Type type, ConstructorInfo info)
        {
            this.type = type;
            this.info = info;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object Invoke(object[] parameters)
        {
            int key = info.MetadataToken;

            if (handler != null)
            {
                return handler(parameters);
            }

            if (DynamicCacheFactory<DynamicConstructorInfoHandler>.Instance.Contains(key))
            {
                this.handler = DynamicCacheFactory<DynamicConstructorInfoHandler>.Instance.GetValue(key);
            }
            else
            {
                this.handler = DynamicMethodFactory.CreateDynamicConstructorInfoHandler(type, info);

                DynamicCacheFactory<DynamicConstructorInfoHandler>.Instance.AddValue(key, this.handler);
            }

            return this.handler(parameters);
        }
    }
}