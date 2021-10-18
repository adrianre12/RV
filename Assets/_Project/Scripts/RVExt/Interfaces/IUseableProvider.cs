using RVModules.RVSmartAI;

namespace RVExt
{
    /// <summary>
    /// This represents agent that can have useable (to aim at, use etc.)
    /// </summary>
    public interface IUseableProvider : IComponent
    {
        #region Properties

        /// <summary>
        /// Selected useable info
        /// </summary>
        UseableInfo Selected { get; set; }

        #endregion
    }
}