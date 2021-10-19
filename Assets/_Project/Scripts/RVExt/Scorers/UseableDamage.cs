using RVHonorAI;
using RVModules.RVSmartAI.GraphElements;

namespace RVExt
{
    public class UseableDamage: AiScorerParams<UseableInfo>
    {
        private IHitPoints _hitPoints;

        public float damageMultiplier = 1;

        protected override void OnContextUpdated()
        {
            base.OnContextUpdated();
            _hitPoints = GetComponentFromContext<IHitPoints>();
        }

/*        public override float Score(float _deltaTime)
        {
            return damageMultiplier * (1 - _hitPoints.HitPoints / _hitPoints.MaxHitPoints);   
        }*/

        protected override float Score(UseableInfo _parameter)
        {
            return damageMultiplier * (1 - _hitPoints.HitPoints / _hitPoints.MaxHitPoints);
        }
    }
}
