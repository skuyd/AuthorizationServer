namespace Pixysoft.Framework.Reflection
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDynamicConstructorInfo
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        object Invoke(object[] parameters);
    }
}