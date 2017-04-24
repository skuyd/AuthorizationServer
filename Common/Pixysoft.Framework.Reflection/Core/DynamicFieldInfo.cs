using System;
using System.Reflection;
using Pixysoft.Framework.Reflection.Controller;

namespace Pixysoft.Framework.Reflection
{
    internal delegate object DynamicFieldGetHandler(object obj);

    internal delegate void DynamicFieldSetHandler(object obj, object value);

    /// <summary>
    /// 
    /// </summary>
    public class DynamicFieldInfo : IDynamicFieldInfo
    {
        /// <summary>
        /// 
        /// </summary>
        private Type type;

        /// <summary>
        /// 
        /// </summary>
        private FieldInfo info;

        /// <summary>
        /// 
        /// </summary>
        private DynamicFieldSetHandler setHandler = null;

        /// <summary>
        /// 
        /// </summary>
        private DynamicFieldGetHandler getHandler = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="info"></param>
        public DynamicFieldInfo(Type type, FieldInfo info)
        {
            this.type = type;
            this.info = info;
        }

        /// <summary>
        /// 
        /// </summary>
        public string FieldName { get { return info.Name; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public object GetValue(object obj)
        {
            int key = info.MetadataToken;

            if (this.getHandler != null)
            {
                return this.getHandler(obj);
            }

            if (DynamicCacheFactory<DynamicFieldGetHandler>.Instance.Contains(key))
            {
                this.getHandler = DynamicCacheFactory<DynamicFieldGetHandler>.Instance.GetValue(key);
            }
            else
            {
                this.getHandler = DynamicMethodFactory.CreateGetHandler(type, info);

                DynamicCacheFactory<DynamicFieldGetHandler>.Instance.AddValue(key, this.getHandler);
            }

            return this.getHandler(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public void SetValue(object obj, object value)
        {
            int key = info.MetadataToken;

            if (this.setHandler != null)
            {
                this.setHandler(obj, value);

                return;
            }

            if (DynamicCacheFactory<DynamicFieldSetHandler>.Instance.Contains(key))
            {
                this.setHandler = DynamicCacheFactory<DynamicFieldSetHandler>.Instance.GetValue(key);
            }
            else
            {
                this.setHandler = DynamicMethodFactory.CreateSetHandler(type, info);

                DynamicCacheFactory<DynamicFieldSetHandler>.Instance.AddValue(key, this.setHandler);
            }

            this.setHandler(obj, value);
        }
    }
}