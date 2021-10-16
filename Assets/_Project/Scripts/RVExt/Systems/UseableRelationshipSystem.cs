using System.Linq;
using RVModules.RVSmartAI;
using UnityEngine;

namespace RVExt
{
    [CreateAssetMenu(fileName = "UseableRelationshipSystem", menuName = "RVExt/Useable Relationship System")]
    public class UseableRelationshipSystem : ScriptableObject
    {

        #region Public methods

        public virtual bool IsUseable(IUseableRelationship _our, IUseableRelationship _other)
        {
            if (_other.GameObject() == _our.GameObject()) return true;
            if (_our.AiUseableGroup == null || _other.AiUseableGroup == null) return false;
            return _our.AiUseableGroup.useableToAll || _other.AiUseableGroup.useableToAll || _our.AiUseableGroup == _other.AiUseableGroup || _our.AiUseableGroup.useable.Contains(_our.AiUseableGroup);
        }

        #endregion
    }
}
