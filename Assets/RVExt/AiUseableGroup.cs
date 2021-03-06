using RVHonorAI;
using UnityEngine;

namespace RVExt
{
    [CreateAssetMenu(fileName = "Ai useable group", menuName = "RVExt/Ai useable group")]
    public class AiUseableGroup : ScriptableObject
    {
        public AiUseableGroup[] useable;

        public AiUseableGroup[] healable;

        public bool useableToAll;

        public bool healToAll;

    }
}
