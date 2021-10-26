using RVModules.RVSmartAI;
using RVModules.RVSmartAI.GraphElements;
using UnityEngine;

namespace RVExt
{
    public class CheckUseableState : AiScorer
    {
        private enum CheckState {
            IsHealable,
            IsUseable
        }

        private IUseableProvider useableProvider;

        [SerializeField]
        private CheckState _checkState = CheckState.IsHealable;

        [SerializeField]
        private float _not;

        protected override string DefaultDescription => "Returns score when selected matches checkState. IsHealable also requres less than 100% HitPoints ";

        protected override void OnContextUpdated()
        {
            useableProvider = ContextAs<IUseableProvider>();
        }

        public override float Score(float _deltaTime)
        {
            switch(_checkState)
            {
                case CheckState.IsHealable:
                    return ScoreHealable();
                case CheckState.IsUseable:
                    return ScoreUseable();
            }
            return _not;
        }

        private float ScoreHealable() { 
            var selected = useableProvider?.Selected;
            if (selected == null || !selected.IsHealable)
                return _not;

            if (selected.Useable.DurabilityRatio() < 1f)
                return score;

            return _not;
        }

        private float ScoreUseable()
        {
            var selected = useableProvider?.Selected;
            if (selected == null || !selected.IsUseable)
                return _not;

            return score;
        }

    }
}