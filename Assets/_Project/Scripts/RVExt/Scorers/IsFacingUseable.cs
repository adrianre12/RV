using RVModules.RVSmartAI.Content.AI.Scorers;
using RVModules.RVUtilities.Extensions;
using UnityEngine;

namespace RVExt
{
    /// <summary>
    /// Make sure ITargetProvider.Target won't be null before using this scorer
    /// Required context: ITargetProvider, IAttackAngle, IMovement
    /// </summary>
    public class IsFacingUseable : AiAgentScorer
    {
        [SerializeField]
        private float notFacingScore;

        [SerializeField]
        private float facingAngle = 5f;

        private IUseableProvider UseableProvider;

        protected override string DefaultDescription => "Make sure IUseableProvider.Useable is not null before using this scorer" +
                                                        "\n Required context: IUseableProvider, IMovement";

        protected override void OnContextUpdated()
        {
            base.OnContextUpdated();
            UseableProvider = GetComponentFromContext<IUseableProvider>();
        }

        public override float Score(float _deltaTime)
        {
            var angle = Vector2.Angle((movement.Rotation * Vector3.forward).ToVector2(), UseableProvider.Selected.Useable.Transform.position.ToVector2() - movement.Position.ToVector2());
            return Mathf.Abs(angle) < facingAngle ? score : notFacingScore;
        }
    }
}