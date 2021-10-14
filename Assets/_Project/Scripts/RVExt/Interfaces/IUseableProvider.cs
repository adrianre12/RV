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
        /// Current useable
        /// </summary>
        IUseable Useable { get; }

        /// <summary>
        /// Current useable info
        /// </summary>
        UseableInfo CurrentUseable { get; set; }

        #endregion
    }
}