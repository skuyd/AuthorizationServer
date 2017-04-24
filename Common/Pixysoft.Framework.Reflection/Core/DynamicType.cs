using System;
using System.Collections.Generic;
using System.Reflection;

namespace Pixysoft.Framework.Reflection
{
    /// <summary>
    /// 
    /// </summary>
    public class DynamicType : IDynamicType
    {
        private Type type = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        public DynamicType(Type type)
        {
            this.type = type;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public IDynamicConstructorInfo GetConstructor(Type[] types)
        {
            ConstructorInfo info = type.GetConstructor(types);

            if (info == null)
                return null;

            return new DynamicConstructorInfo(type, info);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bindingAttr"></param>
        /// <param name="binder"></param>
        /// <param name="types"></param>
        /// <param name="modifiers"></param>
        /// <returns></returns>
        public IDynamicConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
        {
            ConstructorInfo info = type.GetConstructor(bindingAttr, binder, types, modifiers);

            if (info == null)
                return null;

            return new DynamicConstructorInfo(type, info);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bindingAttr"></param>
        /// <param name="binder"></param>
        /// <param name="callConvention"></param>
        /// <param name="types"></param>
        /// <param name="modifiers"></param>
        /// <returns></returns>
        public IDynamicConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
        {
            ConstructorInfo info = type.GetConstructor(bindingAttr, binder, callConvention, types, modifiers);

            if (info == null)
                return null;

            return new DynamicConstructorInfo(type, info);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDynamicConstructorInfo[] GetConstructors()
        {
            List<IDynamicConstructorInfo> list = new List<IDynamicConstructorInfo>();

            foreach (ConstructorInfo info in type.GetConstructors())
            {
                list.Add(new DynamicConstructorInfo(type, info));
            }
            return list.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bindingAttr"></param>
        /// <returns></returns>
        public IDynamicConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
        {
            List<IDynamicConstructorInfo> list = new List<IDynamicConstructorInfo>();
            foreach (ConstructorInfo info in type.GetConstructors(bindingAttr))
            {
                list.Add(new DynamicConstructorInfo(type, info));
            }
            return list.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IDynamicFieldInfo GetField(string name)
        {
            FieldInfo info = type.GetField(name);
            if (info == null)
                return null;
            return new DynamicFieldInfo(type, info);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="bindingAttr"></param>
        /// <returns></returns>
        public IDynamicFieldInfo GetField(string name, BindingFlags bindingAttr)
        {
            FieldInfo info = type.GetField(name, bindingAttr);
            if (info == null)
                return null;
            return new DynamicFieldInfo(type, info);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDynamicFieldInfo[] GetFields()
        {
            List<IDynamicFieldInfo> list = new List<IDynamicFieldInfo>();
            foreach (FieldInfo info in type.GetFields())
            {
                list.Add(new DynamicFieldInfo(type, info));
            }
            return list.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bindingAttr"></param>
        /// <returns></returns>
        public IDynamicFieldInfo[] GetFields(BindingFlags bindingAttr)
        {
            List<IDynamicFieldInfo> list = new List<IDynamicFieldInfo>();
            foreach (FieldInfo info in type.GetFields(bindingAttr))
            {
                list.Add(new DynamicFieldInfo(type, info));
            }
            return list.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IDynamicPropertyInfo GetProperty(string name)
        {
            PropertyInfo info = type.GetProperty(name);
            if (info == null)
                return null;
            return new DynamicPropertyInfo(type, info);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="bindingAttr"></param>
        /// <returns></returns>
        public IDynamicPropertyInfo GetProperty(string name, BindingFlags bindingAttr)
        {
            PropertyInfo info = type.GetProperty(name, bindingAttr);
            if (info == null)
                return null;
            return new DynamicPropertyInfo(type, info);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="returnType"></param>
        /// <returns></returns>
        public IDynamicPropertyInfo GetProperty(string name, Type returnType)
        {
            PropertyInfo info = type.GetProperty(name, returnType);
            if (info == null)
                return null;
            return new DynamicPropertyInfo(type, info);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public IDynamicPropertyInfo GetProperty(string name, Type[] types)
        {
            PropertyInfo info = type.GetProperty(name, types);
            if (info == null)
                return null;
            return new DynamicPropertyInfo(type, info);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="returnType"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public IDynamicPropertyInfo GetProperty(string name, Type returnType, Type[] types)
        {
            PropertyInfo info = type.GetProperty(name, returnType, types);
            if (info == null)
                return null;
            return new DynamicPropertyInfo(type, info);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="returnType"></param>
        /// <param name="types"></param>
        /// <param name="modifiers"></param>
        /// <returns></returns>
        public IDynamicPropertyInfo GetProperty(string name, Type returnType, Type[] types, ParameterModifier[] modifiers)
        {
            PropertyInfo info = type.GetProperty(name, returnType, types, modifiers);
            if (info == null)
                return null;
            return new DynamicPropertyInfo(type, info);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="bindingAttr"></param>
        /// <param name="binder"></param>
        /// <param name="returnType"></param>
        /// <param name="types"></param>
        /// <param name="modifiers"></param>
        /// <returns></returns>
        public IDynamicPropertyInfo GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
        {
            PropertyInfo info = type.GetProperty(name, bindingAttr, binder, returnType, types, modifiers);
            if (info == null)
                return null;
            return new DynamicPropertyInfo(type, info);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDynamicPropertyInfo[] GetProperties()
        {
            List<IDynamicPropertyInfo> list = new List<IDynamicPropertyInfo>();
            foreach (PropertyInfo info in type.GetProperties())
            {
                list.Add(new DynamicPropertyInfo(type, info));
            }
            return list.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bindingAttr"></param>
        /// <returns></returns>
        public IDynamicPropertyInfo[] GetProperties(BindingFlags bindingAttr)
        {
            List<IDynamicPropertyInfo> list = new List<IDynamicPropertyInfo>();
            foreach (PropertyInfo info in type.GetProperties(bindingAttr))
            {
                list.Add(new DynamicPropertyInfo(type, info));
            }
            return list.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDynamicMethodInfo[] GetMethods()
        {
            MethodInfo[] infos = type.GetMethods();
            List<IDynamicMethodInfo> list = new List<IDynamicMethodInfo>();
            foreach (var info in infos)
            {
                list.Add(new DynamicMethodInfo(type, info));
            }
            return list.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IDynamicMethodInfo GetMethods(string name)
        {
            MethodInfo info = type.GetMethod(name);
            if (info == null)
                return null;
            return new DynamicMethodInfo(type, info);
        }
    }
}