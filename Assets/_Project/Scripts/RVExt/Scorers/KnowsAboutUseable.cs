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


        protected override string DefaultDescription => "Returns score when either IsUseable is true or if IsHealable and HitPoints is less than 100%";

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
                    if (useableInfo.IsUseable)
                        return score;
                    if (useableInfo.IsHealable && useableInfo.Useable.DurabilityRatio() < 1f)
                        return score;
                }
            }

            return not;
        }

    }
}