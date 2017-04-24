using System;
using System.Reflection;
using Pixysoft.Framework.Reflection.Controller;

namespace Pixysoft.Framework.Reflection
{
    internal delegate object DynamicMethodGetHandler(object obj, object[] parameters);

    /// <summary>
    ///
    /// </summary>
    public class DynamicMethodInfo : IDynamicMethodInfo
    {
        private Type type;

        private MethodInfo info;

        private DynamicMethodGetHandler getHandler = null;

        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        /// <param name="info"></param>
        public DynamicMethodInfo(Type type, MethodInfo info)
        {
            this.type = type;

            this.info = info;
        }

        /// <summary>
        /// 原属性
        /// </summary>
        public MethodInfo Info
        {
            get
            {
                return this.info;
            }
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object Invoke(object obj, object[] parameters)
        {
            int key = info.MetadataToken;

            if (this.getHandler != null)
            {
                return this.getHandler(obj, parameters);
            }

            if (DynamicCacheFactory<DynamicMethodGetHandler>.Instance.Contains(key))
            {
                this.getHandler = DynamicCacheFactory<DynamicMethodGetHandler>.Instance.GetValue(key);
            }
            else
            {
                this.getHandler = DynamicMethodFactory.CreateMethodHandler(type, info);

                DynamicCacheFactory<DynamicMethodGetHandler>.Instance.AddValue(key, this.getHandler);
            }

            return this.getHandler(obj, parameters);
        }
    }
}