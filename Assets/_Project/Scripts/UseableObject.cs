using UnityEngine;
using RVModules.RVSmartAI.Content.Scanners;
using RVHonorAI;
using RVHonorAI.Combat;
using RVHonorAI.Systems;
using RVModules.RVCommonGameLibrary.Audio;

namespace RVExt
{
    public class UseableObject : MonoBehaviour, IScannable, IUseable, IUseableRelationship //, ITarget, IRelationship //, IDamageable, ITargetProvider, IHitPoints
    {
        private float _radius = .5f;

        /*        [SerializeField]
                private AiGroup group;

                [SerializeField]
                private RelationshipSystem _relationshipSystem;
        */

        [SerializeField]
        private Transform _aimTransform;


        [SerializeField]
        private UseableRelationshipSystem _useableRelationshipSystem;

        [SerializeField]
        private AiUseableGroup _aiUseableGroup;

        public UseableRelationshipSystem UseableRelationshipSystem => _useableRelationshipSystem;

        //ITarget
        public float Radius => _radius;

        public Transform Transform => transform;

        public Transform AimTransform => _aimTransform;

        public float Danger => 0f;

        public Transform VisibilityCheckTransform => _aimTransform;

/*        AiGroup IRelationship.AiGroup
        {
            get { return group; }
            set { }
        }*/
        public bool TreatNeutralCharactersAsEnemies => false;

        public AiUseableGroup AiUseableGroup
        {
            get => _aiUseableGroup;
            set => _aiUseableGroup = value;
        }

        public bool IsUseable(IUseableRelationship _other) => _useableRelationshipSystem.IsUseable(this, _other);


        //IRelationship
        //public bool IsAlly(IRelationship _other) => _relationshipSystem.IsAlly(this, _other);

        //public bool IsEnemy(IRelationship _other, bool _contraCheck = false) => _relationshipSystem.IsEnemy(this, _other, _contraCheck);


    }
}
