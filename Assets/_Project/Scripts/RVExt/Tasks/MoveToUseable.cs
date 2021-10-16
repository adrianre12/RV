using RVModules.RVSmartAI.Content.AI.Tasks;

namespace RVExt
{
    /// <summary>
    /// Moves  to useable's position or it's last seen position if it is not visible
    /// </summary>
    public class MoveToUseable : AiAgentTask
    {
        private IUseableProvider useableProvider;

        protected override string DefaultDescription => "Moves  to useable's position or it's last seen position if it is not visible";

        protected override void OnContextUpdated()
        {
            base.OnContextUpdated();
            useableProvider = GetComponentFromContext<IUseableProvider>();
        }

        protected override void Execute(float _deltaTime)
        {
            movement.Destination = useableProvider.CurrentUseable.Visible ? useableProvider.Useable.Transform.position : useableProvider.CurrentUseable.LastSeenPosition;
        }
    }
}