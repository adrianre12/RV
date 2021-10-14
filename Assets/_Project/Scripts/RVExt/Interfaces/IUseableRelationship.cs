using RVModules.RVSmartAI;

namespace RVExt
{
    /// <summary>
    /// todo desc
    /// </summary>
    public interface IUseableRelationship : IComponent
    {
        #region Properties

        /// <summary>
        /// Our ai group
        /// </summary>
        AiUseableGroup AiUseableGroup { get; set; }

        #endregion

        #region Public methods

        /// <summary>
        /// Check's relationship to other
        /// </summary>
        bool IsUseable(IUseableRelationship _other);

        #endregion
    }
}
