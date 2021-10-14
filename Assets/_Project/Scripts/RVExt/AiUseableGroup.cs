using RVHonorAI;
using UnityEngine;

namespace RVExt
{
    [CreateAssetMenu(fileName = "Ai useable group", menuName = "RVExt/Ai useable group")]
    public class AiUseableGroup : ScriptableObject
    {
        #region Fields
        public AiUseableGroup[] useable;

        public bool useableToAll;

        #endregion
    }
}
