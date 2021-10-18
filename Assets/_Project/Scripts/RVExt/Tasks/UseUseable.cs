using RVModules.RVSmartAI.GraphElements;

namespace RVExt
{
    public class UseUseable : AiTask
    {
        private IUseableCharacter _useableCharacter;

        protected override void OnContextUpdated()
        {
            _useableCharacter = GetComponentFromContext<IUseableCharacter>();
        }

        protected override void Execute(float _deltaTime)
        {
            _useableCharacter.Selected.Useable.Use(_useableCharacter.MyGameObject);
        }
    }
}
