using RVModules.RVSmartAI;
using RVModules.RVSmartAI.GraphElements;
using UnityEngine;

namespace RVExt
{
    public class CanSeeUseable : AiScorer
    {
        #region Fields

        private IUseableProvider useableProvider;

        [SerializeField]
        protected float not;

        #endregion

        #region Properties

        protected override string DefaultDescription => "Returns set score if we can see current useable";

        #endregion

        #region Public methods

        public override float Score(float _deltaTime)
        {
            if (useableProvider.Useable.Object() == null) return not;
            return useableProvider.CurrentUseable.Visible ? score : not;
        }

        #endregion

        #region Not public methods

        protected override void OnContextUpdated() => useableProvider = GetComponentFromContext<IUseableProvider>();

        #endregion
    }
}