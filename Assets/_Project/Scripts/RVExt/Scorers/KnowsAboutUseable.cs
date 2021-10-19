using RVModules.RVSmartAI;
using RVModules.RVSmartAI.GraphElements;
using UnityEngine;

namespace RVExt
{
    public class KnowsAboutUseable : AiScorer
    {
        private IUseableInfosProvider useableInfosProvider;

        [SerializeField]
        private float not;

        [Tooltip("Look for undammaged useables")]
        [SerializeField]
        private bool notDamaged = true;

        [Tooltip("Look for dammaged useables")]
        [SerializeField]
        private bool damaged = true;

        protected override string DefaultDescription => "Returns score when IUseableInfosProvider.UseableInfos has any entries" +
                                                        "\n Required context: IUseableInfosProvider";

        protected override void OnContextUpdated()
        {
            useableInfosProvider = ContextAs<IUseableInfosProvider>();
        }

        public override float Score(float _deltaTime)
        {
            foreach (var useableInfo in useableInfosProvider.UseableInfos)
            {


                if (useableInfo != null && useableInfo.Useable.Object() != null)
                {
                    if (damaged && useableInfo.Useable.DurabilityRatio() < 1f)
                        return score;
                    if (notDamaged && useableInfo.Useable.DurabilityRatio() == 1f)
                        return score;
                }
            }

            return not;
        }

    }
}