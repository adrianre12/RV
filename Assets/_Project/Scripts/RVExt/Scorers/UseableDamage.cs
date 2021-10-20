using RVHonorAI;
using RVModules.RVSmartAI.GraphElements;

namespace RVExt
{
    public class UseableDamage: AiScorerParams<UseableInfo>
    {
        private IHitPoints _hitPoints;

        protected override string DefaultDescription => "Returns score scaled by the HitPoint loss, no damage done = 0";

        protected override void OnContextUpdated()
        {
            base.OnContextUpdated();
            _hitPoints = ContextAs<IHitPoints>();
        }

        protected override float Score(UseableInfo _parameter)
        {
            return score * (1 - _parameter.Useable.DurabilityRatio());
        }
    }
}
