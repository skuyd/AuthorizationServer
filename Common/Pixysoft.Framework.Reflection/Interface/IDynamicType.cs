using System;
using System.Reflection;

namespace Pixysoft.Framework.Reflection
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDynamicType
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        IDynamicConstructorInfo GetConstructor(Type[] types);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bindingAttr"></param>
        /// <param name="binder"></param>
        /// <param name="types"></param>
        /// <param name="modifiers"></param>
        /// <returns></returns>
        IDynamicConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bindingAttr"></param>
        /// <param name="binder"></param>
        /// <param name="callConvention"></param>
        /// <param name="types"></param>
        /// <param name="modifiers"></param>
        /// <returns></returns>
        IDynamicConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IDynamicConstructorInfo[] GetConstructors();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bindingAttr"></param>
        /// <returns></returns>
        IDynamicConstructorInfo[] GetConstructors(BindingFlags bindingAttr);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IDynamicFieldInfo GetField(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="bindingAttr"></param>
        /// <returns></returns>
        IDynamicFieldInfo GetField(string name, BindingFlags bindingAttr);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IDynamicFieldInfo[] GetFields();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bindingAttr"></param>
        /// <returns></returns>
        IDynamicFieldInfo[] GetFields(BindingFlags bindingAttr);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IDynamicPropertyInfo GetProperty(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="bindingAttr"></param>
        /// <returns></returns>
        IDynamicPropertyInfo GetProperty(string name, BindingFlags bindingAttr);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="returnType"></param>
        /// <returns></returns>
        IDynamicPropertyInfo GetProperty(string name, Type returnType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        IDynamicPropertyInfo GetProperty(string name, Type[] types);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="returnType"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        IDynamicPropertyInfo GetProperty(string name, Type returnType, Type[] types);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="returnType"></param>
        /// <param name="types"></param>
        /// <param name="modifiers"></param>
        /// <returns></returns>
        IDynamicPropertyInfo GetProperty(string name, Type returnType, Type[] types, ParameterModifier[] modifiers);

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
        IDynamicPropertyInfo GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IDynamicPropertyInfo[] GetProperties();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bindingAttr"></param>
        /// <returns></returns>
        IDynamicPropertyInfo[] GetProperties(BindingFlags bindingAttr);
    }
}