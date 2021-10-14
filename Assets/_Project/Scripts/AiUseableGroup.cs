using RVHonorAI;
using UnityEngine;

namespace RVExt
{
    [CreateAssetMenu(fileName = "Ai useable group", menuName = "RVExt/Ai useable group")]
    public class AiUseableGroup : ScriptableObject
    {
        #region Fields

        public AiUseableGroup[] allies;
        public AiUseableGroup[] enemies;
        public AiUseableGroup[] useable;

        public bool enemyToAll;
        public bool allyToAll;
        public bool useableToAll;

        #endregion
    }
}
