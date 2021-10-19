using RVModules.RVSmartAI.GraphElements;

namespace RVExt
{
    public class HealUseable : AiTask
    {
        private IUseableCharacter _useableCharacter;

        public float healAmount;

        protected override void OnContextUpdated()
        {
            _useableCharacter = ContextAs<IUseableCharacter>();
        }

        protected override void Execute(float _deltaTime)
        {
            _useableCharacter.Selected.Useable.Heal(healAmount);
        }
    }
}
