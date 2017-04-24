using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Pixysoft.Framework.Reflection.Controller
{
    internal class DynamicMethodFactory
    {
        /// <summary>
        /// 创建构造函数
        /// </summary>
        /// <param name="type"></param>
        /// <param name="constructorInfo"></param>
        /// <returns></returns>
        internal static DynamicConstructorInfoHandler CreateDynamicConstructorInfoHandler(Type type, ConstructorInfo constructorInfo)
        {
            int argIndex = 0;
            DynamicMethod dynamicMethod = new DynamicMethod("DynamicConstructor",
                MethodAttributes.Static | MethodAttributes.Public, CallingConventions.Standard, typeof(object), new Type[] { typeof(object[]) }, type, true);
            ILGenerator generator = dynamicMethod.GetILGenerator();

            foreach (ParameterInfo parainfo in constructorInfo.GetParameters())
            {
                generator.Emit(OpCodes.Ldarg_0);
                if (argIndex > 8)
                    generator.Emit(OpCodesFactory.GetLdc_I4(argIndex), argIndex);
                else
                    generator.Emit(OpCodesFactory.GetLdc_I4(argIndex));
                generator.Emit(OpCodes.Ldelem_Ref);
                OpCodesFactory.UnboxIfNeeded(generator, parainfo.ParameterType);
                argIndex++;
            }
            generator.Emit(OpCodes.Newobj, constructorInfo);
            generator.Emit(OpCodes.Ret);
            return (DynamicConstructorInfoHandler)dynamicMethod.CreateDelegate(typeof(DynamicConstructorInfoHandler));
        }

        /// <summary>
        /// 创建PropertyInfo的动态方法get
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        internal static DynamicPropertyGetHandler CreateGetHandler(Type type, PropertyInfo propertyInfo)
        {
            MethodInfo getMethodInfo = propertyInfo.GetGetMethod(true);
            int argIndex = 0;

            DynamicMethod dynamicGet = new DynamicMethod("DynamicGet", typeof(object), new Type[] { typeof(object), typeof(object[]) }, type, true);
            ILGenerator getGenerator = dynamicGet.GetILGenerator();

            getGenerator.Emit(OpCodes.Ldarg_0);
            foreach (ParameterInfo parainfo in getMethodInfo.GetParameters())
            {
                getGenerator.Emit(OpCodes.Ldarg_1);
                if (argIndex > 8)
                    getGenerator.Emit(OpCodesFactory.GetLdc_I4(argIndex), argIndex);
                else
                    getGenerator.Emit(OpCodesFactory.GetLdc_I4(argIndex));
                getGenerator.Emit(OpCodes.Ldelem_Ref);
                OpCodesFactory.UnboxIfNeeded(getGenerator, parainfo.ParameterType);
                argIndex++;
            }
            getGenerator.Emit(OpCodes.Callvirt, getMethodInfo);
            OpCodesFactory.BoxIfNeeded(getGenerator, getMethodInfo.ReturnType);
            getGenerator.Emit(OpCodes.Ret);

            return (DynamicPropertyGetHandler)dynamicGet.CreateDelegate(typeof(DynamicPropertyGetHandler));
        }

        /// <summary>
        /// 创建PropertyInfo的动态方法get
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        internal static DynamicPropertySetHandler CreateSetHandler(Type type, PropertyInfo propertyInfo)
        {
            MethodInfo setMethodInfo = propertyInfo.GetSetMethod(true);
            int argCount = setMethodInfo.GetParameters().Length;
            int argIndex = 0;

            DynamicMethod dynamicSet = new DynamicMethod("DynamicSet", typeof(void), new Type[] { typeof(object), typeof(object), typeof(object[]) }, type, true);
            ILGenerator setGenerator = dynamicSet.GetILGenerator();

            setGenerator.Emit(OpCodes.Ldarg_0);
            foreach (ParameterInfo parainfo in setMethodInfo.GetParameters())
            {
                if (argIndex + 1 >= argCount)
                    break;

                setGenerator.Emit(OpCodes.Ldarg_2);
                if (argIndex > 8)
                    setGenerator.Emit(OpCodesFactory.GetLdc_I4(argIndex), argIndex);
                else
                    setGenerator.Emit(OpCodesFactory.GetLdc_I4(argIndex));
                setGenerator.Emit(OpCodes.Ldelem_Ref);
                OpCodesFactory.UnboxIfNeeded(setGenerator, parainfo.ParameterType);
                argIndex++;
            }
            setGenerator.Emit(OpCodes.Ldarg_1);
            OpCodesFactory.UnboxIfNeeded(setGenerator, setMethodInfo.GetParameters()[argIndex].ParameterType);
            setGenerator.Emit(OpCodes.Call, setMethodInfo);
            setGenerator.Emit(OpCodes.Ret);

            return (DynamicPropertySetHandler)dynamicSet.CreateDelegate(typeof(DynamicPropertySetHandler));
        }

        /// <summary>
        /// 创建Field的动态方法get
        /// </summary>
        /// <param name="type"></param>
        /// <param name="fieldInfo"></param>
        /// <returns></returns>
        internal static DynamicFieldGetHandler CreateGetHandler(Type type, FieldInfo fieldInfo)
        {
            DynamicMethod dynamicGet = CreateGetDynamicMethod(type);
            ILGenerator getGenerator = dynamicGet.GetILGenerator();

            getGenerator.Emit(OpCodes.Ldarg_0);
            getGenerator.Emit(OpCodes.Ldfld, fieldInfo);
            OpCodesFactory.BoxIfNeeded(getGenerator, fieldInfo.FieldType);
            getGenerator.Emit(OpCodes.Ret);

            return (DynamicFieldGetHandler)dynamicGet.CreateDelegate(typeof(DynamicFieldGetHandler));
        }

        /// <summary>
        /// 创建Field的动态方法set
        /// </summary>
        /// <param name="type"></param>
        /// <param name="fieldInfo"></param>
        /// <returns></returns>
        internal static DynamicFieldSetHandler CreateSetHandler(Type type, FieldInfo fieldInfo)
        {
            DynamicMethod dynamicSet = CreateSetDynamicMethod(type);
            ILGenerator setGenerator = dynamicSet.GetILGenerator();

            setGenerator.Emit(OpCodes.Ldarg_0);
            setGenerator.Emit(OpCodes.Ldarg_1);
            OpCodesFactory.UnboxIfNeeded(setGenerator, fieldInfo.FieldType);
            setGenerator.Emit(OpCodes.Stfld, fieldInfo);
            setGenerator.Emit(OpCodes.Ret);

            return (DynamicFieldSetHandler)dynamicSet.CreateDelegate(typeof(DynamicFieldSetHandler));
        }

        /// <summary>
        /// 创建MethodInfo的动态方法
        /// </summary>
        /// <param name="type"></param>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        internal static DynamicMethodGetHandler CreateMethodHandler(Type type, MethodInfo methodInfo)
        {
            int argIndex = 0;
            int parasNum = methodInfo.GetParameters().Length;
            parasNum = parasNum < 2 ? 2 : parasNum;
            Type[] paras = new Type[parasNum];
            for (int i = 0; i < parasNum; i++)
            {
                paras[i] = typeof(object);
            }
            DynamicMethod dynamicGet = new DynamicMethod("DynamicGet", typeof(object), paras, type, true);
            ILGenerator getGenerator = dynamicGet.GetILGenerator();

            getGenerator.Emit(OpCodes.Ldarg_0);
            foreach (ParameterInfo parainfo in methodInfo.GetParameters())
            {
                getGenerator.Emit(OpCodes.Ldarg_1);
                if (argIndex > 8)
                    getGenerator.Emit(OpCodesFactory.GetLdc_I4(argIndex), argIndex);
                else
                    getGenerator.Emit(OpCodesFactory.GetLdc_I4(argIndex));
                getGenerator.Emit(OpCodes.Ldelem_Ref);
                OpCodesFactory.UnboxIfNeeded(getGenerator, parainfo.ParameterType);
                argIndex++;
            }
            getGenerator.Emit(OpCodes.Callvirt, methodInfo);
            OpCodesFactory.BoxIfNeeded(getGenerator, methodInfo.ReturnType);
            getGenerator.Emit(OpCodes.Ret);

            return (DynamicMethodGetHandler)dynamicGet.CreateDelegate(typeof(DynamicMethodGetHandler));
        }

        private static DynamicMethod CreateGetDynamicMethod(Type type)
        {
            return new DynamicMethod("DynamicGet", typeof(object), new Type[] { typeof(object) }, type, true);
        }

        private static DynamicMethod CreateSetDynamicMethod(Type type)
        {
            return new DynamicMethod("DynamicSet", typeof(void), new Type[] { typeof(object), typeof(object) }, type, true);
        }
    }
}