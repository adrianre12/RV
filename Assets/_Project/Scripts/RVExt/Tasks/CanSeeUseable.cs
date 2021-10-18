using RVModules.RVSmartAI;
using RVModules.RVSmartAI.GraphElements;
using UnityEngine;

namespace RVExt
{
    public class CanSeeUseable : AiScorer
    {
        private IUseableProvider _useableProvider;


        [SerializeField]
        protected float not;

        protected override string DefaultDescription => "Returns set score if we can see current useable";

        protected override void OnContextUpdated()
        {
            _useableProvider = GetComponentFromContext<IUseableProvider>();
        }

        public override float Score(float _deltaTime)
        {
            /*            if (useableProvider.Useable.Object() == null) return not;
                        return useableProvider.Selected.Visible ? score : not;*/
            var _selected = _useableProvider?.Selected;
            if (_selected == null || _selected.Useable.Object() == null) return not;
            return _selected.Visible ? score : not;
        }
    }
}