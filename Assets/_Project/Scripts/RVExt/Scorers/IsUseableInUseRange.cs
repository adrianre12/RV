using RVModules.RVSmartAI.Content.AI.Scorers;
using RVModules.RVUtilities.Extensions;
using UnityEngine;

namespace RVExt
{
    public class IsUseableInUseRange: AiAgentScorer
    {
        private IUseableProvider _useableProvider;

        public float scoreNotInRange;

        protected override void OnContextUpdated()
        {
            base.OnContextUpdated();
            _useableProvider = GetComponentFromContext<IUseableProvider>();
        }

        public override float Score(float _deltaTime)
        {
            if (_useableProvider.Selected == null) 
                return scoreNotInRange;
            IUseable useable = _useableProvider.Selected.Useable;
            bool arrived = Vector2.Distance(movement.Position.ToVector2(), useable.UseTransform.position.ToVector2()) < useable.Radius;
            return arrived ? score : scoreNotInRange;
        }
    }
}
