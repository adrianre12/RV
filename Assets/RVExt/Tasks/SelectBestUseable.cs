using System.Collections.Generic;
using RVModules.RVSmartAI.GraphElements;
using UnityEngine;

namespace RVExt
{
    /// <summary>
    /// Sets IUseableProvider.UseableInfo to the highest scored one from IUseableInfosProvider.UseableInfos
    /// </summary>
    public class SelectBestUseable : AiTaskParams<UseableInfo>
    {
        #region Fields

        private IUseableInfosProvider useableInfosProvider;
        private IUseableProvider useableProvider;
        private List<UseableInfo> nonNullUseables = new List<UseableInfo>();

        [Tooltip("Look for undammaged useables")]
        [SerializeField]
        private bool notDamaged = true;

        [Tooltip("Look for dammaged useables")]
        [SerializeField]
        private bool damaged = true;

        #endregion

        #region Properties

        protected override string DefaultDescription => "Sets IUseableProvider.UseableInfo to the highest scored one from IUseableInfosProvider.UseableInfos" +
                                                        "\n Required context: IUseableInfosProvider, IUseableProvider";

        #endregion

        #region Not public methods

        protected override void OnContextUpdated()
        {
            useableInfosProvider = ContextAs<IUseableInfosProvider>();
            useableProvider = ContextAs<IUseableProvider>();
        }

        protected override void Execute(float _deltaTime)
        {
            nonNullUseables.Clear();
            // remove null useables as we cant rely on scanning
            for (var i = 0; i < useableInfosProvider.UseableInfos.Count; i++)
            {
                var useableInfo = useableInfosProvider.UseableInfos[i];
                if (useableInfo.Useable as Object == null) continue;
                if (damaged && useableInfo.Useable.DurabilityRatio() < 1f)
                    nonNullUseables.Add(useableInfo);
                if (notDamaged && useableInfo.Useable.DurabilityRatio() == 1f)
                    nonNullUseables.Add(useableInfo);
            }
            if (nonNullUseables.Count == 0)
                useableProvider.Selected = null;
            else
                useableProvider.Selected = GetBest(nonNullUseables);
        }

        #endregion
    }
}
