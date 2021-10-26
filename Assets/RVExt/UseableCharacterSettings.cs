using UnityEngine;

namespace RVExt
{
    public class UseableCharacterSettings : MonoBehaviour
    {
        [SerializeField]
        public UseableRelationshipSystem useableRelationshipSystem;

        [SerializeField]
        public AiUseableGroup aiUseableGroup;

        [SerializeField]
        public float healAmmount = 50f;
    }
}
