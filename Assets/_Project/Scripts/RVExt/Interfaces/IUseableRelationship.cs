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

        /// <summary>
        /// If other character is not ally to this, should it be treated as enemy
        /// </summary>
        bool TreatNeutralCharactersAsEnemies { get; }

        #endregion

        #region Public methods

        /// <summary>
        /// Check's relationship to other
        /// </summary>
        bool IsEnemy(IUseableRelationship _other, bool _contraCheck = false);

        /// <summary>
        /// Check's relationship to other
        /// </summary>
        bool IsAlly(IUseableRelationship _other);

        /// <summary>
        /// Check's relationship to other
        /// </summary>
        bool IsUseable(IUseableRelationship _other);

        #endregion
    }
}
