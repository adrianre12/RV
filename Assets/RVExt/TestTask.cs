using RVExt;
using RVModules.RVSmartAI.GraphElements;
using UnityEngine;

public class TestTask : AiTask
{
    private IUseableCharacter useableCharacter;

    protected override void OnContextUpdated()
    {
        base.OnContextUpdated();
        useableCharacter = GetComponentFromContext<IUseableCharacter>();
    }

    protected override void Execute(float _deltaTime)
    {
        Debug.Log(useableCharacter.UseableInfos.Count);
    }

}
