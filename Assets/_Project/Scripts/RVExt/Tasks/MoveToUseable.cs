using RVModules.RVSmartAI.Content.AI.Tasks;

namespace RVExt
{
    /// <summary>
    /// Moves  to useable's position or it's last seen position if it is not visible
    /// </summary>
    public class MoveToUseable : AiAgentTask
    {
        private IUseableProvider _useableProvider;

        protected override string DefaultDescription => "Moves  to useable's position or it's last seen position if it is not visible";

        protected override void OnContextUpdated()
        {
            base.OnContextUpdated();
            _useableProvider = ContextAs<IUseableProvider>();

        }

        protected override void Execute(float _deltaTime)
        {
            var _selected = _useableProvider?.Selected;
            if (_selected == null)
                return;
            //movement.Destination = useableProvider.Selected.Visible ? useableProvider.Selected.Useable.UseTransform.position : useableProvider.Selected.LastSeenPosition;
            movement.Destination = _selected.Visible ? _selected.Useable.UseTransform.position : _selected.LastSeenPosition;

        }
    }
}