using System.Linq;
using RVModules.RVSmartAI;
using UnityEngine;

namespace RVExt
{
    //[CreateAssetMenu(fileName = "UseableRelationshipSystem", menuName = "RVExt/Useable Relationship System")]
    public class UseableRelationshipSystem : ScriptableObject
    {

        #region Public methods

        public virtual bool IsEnemy(IUseableRelationship _our, IUseableRelationship _other, bool _contraCheck)
        {
            // check if this is self-check, we have to compare game object, because we don't know which component may implement IRelationship
            if (_other.GameObject() == _our.GameObject()) return false;

            // check for both sides-relationship
            if (!_contraCheck && _other.IsEnemy(_our, true)) return true;

            if (_our.TreatNeutralCharactersAsEnemies) return !_our.IsAlly(_other);

            if (_our.AiUseableGroup == null || _other.AiUseableGroup == null || _our.AiUseableGroup == _other.AiUseableGroup) return false;

            return _other.AiUseableGroup.enemyToAll || _our.AiUseableGroup.enemies.Contains(_other.AiUseableGroup) || _other.AiUseableGroup.enemies.Contains(_our.AiUseableGroup);
        }

        public virtual bool IsAlly(IUseableRelationship _our, IUseableRelationship _other)
        {
            if (_other.GameObject() == _our.GameObject()) return true;
            if (_our.AiUseableGroup == null || _other.AiUseableGroup == null) return false;
            return _other.AiUseableGroup.allyToAll || _our.AiUseableGroup == _other.AiUseableGroup || _our.AiUseableGroup.allies.Contains(_our.AiUseableGroup);
        }

        public virtual bool IsUseable(IUseableRelationship _our, IUseableRelationship _other)
        {
            if (_other.GameObject() == _our.GameObject()) return true;
            if (_our.AiUseableGroup == null || _other.AiUseableGroup == null) return false;
            return _other.AiUseableGroup.useableToAll || _our.AiUseableGroup == _other.AiUseableGroup || _our.AiUseableGroup.useable.Contains(_our.AiUseableGroup);
        }

        #endregion
    }
}
