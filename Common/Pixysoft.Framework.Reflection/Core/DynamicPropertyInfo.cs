using System;
using System.Reflection;
using Pixysoft.Framework.Reflection.Controller;

namespace Pixysoft.Framework.Reflection
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="value"></param>
    /// <param name="index"></param>
    internal delegate void DynamicPropertySetHandler(object obj, object value, object[] index);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    internal delegate object DynamicPropertyGetHandler(object obj, object[] index);

    /// <summary>
    /// 
    /// </summary>
    public class DynamicPropertyInfo : IDynamicPropertyInfo
    {
        private Type type;

        private PropertyInfo info;

        /// <summary>
        /// 原属性
        /// </summary>
        public PropertyInfo Info
        {
            get
            {
                return this.info;
            }
        }

        private DynamicPropertySetHandler setHandler = null;

        private DynamicPropertyGetHandler getHandler = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="info"></param>
        public DynamicPropertyInfo(Type type, PropertyInfo info)
        {
            this.type = type;

            this.info = info;
        }

        /// <summary>
        /// 属性名称
        /// </summary>
        public string Name
        {
            get
            {
                return this.info.Name;
            }
        }

        /// <summary>
        /// 属性数据类型
        /// </summary>
        public Type PropertyType
        {
            get
            {
                return this.info.PropertyType;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public object GetValue(object obj, object[] index)
        {
            int key = info.MetadataToken;

            if (this.getHandler != null)
            {
                return this.getHandler(obj, index);
            }

            if (DynamicCacheFactory<DynamicPropertyGetHandler>.Instance.Contains(key))
            {
                this.getHandler = DynamicCacheFactory<DynamicPropertyGetHandler>.Instance.GetValue(key);
            }
            else
            {
                this.getHandler = DynamicMethodFactory.CreateGetHandler(type, info);
                //DynamicCacheFactory<DynamicPropertyGetHandler>.Instance.AddValue(key, this.getHandler);
            }
            return this.getHandler(obj, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        /// <param name="index"></param>
        public void SetValue(object obj, object value, object[] index)
        {
            int key = info.MetadataToken;

            if (this.setHandler != null)
            {
                this.setHandler(obj, value, index);

                return;
            }

            if (DynamicCacheFactory<DynamicPropertySetHandler>.Instance.Contains(key))
            {
                this.setHandler = DynamicCacheFactory<DynamicPropertySetHandler>.Instance.GetValue(key);
            }
            else
            {
                this.setHandler = DynamicMethodFactory.CreateSetHandler(type, info);
                //DynamicCacheFactory<DynamicPropertySetHandler>.Instance.AddValue(key, this.setHandler);
            }
            this.setHandler(obj, value, index);
        }
    }
}