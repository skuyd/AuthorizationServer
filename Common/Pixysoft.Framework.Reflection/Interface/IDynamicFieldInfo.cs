namespace Pixysoft.Framework.Reflection
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDynamicFieldInfo
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        object GetValue(object obj);

        /// <summary>
        /// 
        /// </summary>
        string FieldName { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        void SetValue(object obj, object value);
    }
}