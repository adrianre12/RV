using RVModules.RVSmartAI;
using RVModules.RVSmartAI.GraphElements;
using UnityEngine;

namespace RVExt
{
    public class KnowsAboutUseable : AiScorer
    {
        private IUseableInfosProvider useableInfosProvider;

        [SerializeField]
        protected float not;

        protected override string DefaultDescription => "Returns score when IUseableInfosProvider.UseableInfos has any entries" +
                                                        "\n Required context: IUseableInfosProvider";

        protected override void OnContextUpdated()
        {
            useableInfosProvider = GetComponentFromContext<IUseableInfosProvider>();
        }

        public override float Score(float _deltaTime)
        {
            foreach (var useableInfo in useableInfosProvider.UseableInfos)
            {
                if (useableInfo != null && useableInfo.Useable.Object() != null) return score;
            }

            return not;
        }

    }
}