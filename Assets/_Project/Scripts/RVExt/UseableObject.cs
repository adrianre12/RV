using UnityEngine;
using RVModules.RVSmartAI.Content.Scanners;
using RVHonorAI;
using RVHonorAI.Combat;
using RVHonorAI.Systems;
using RVModules.RVCommonGameLibrary.Audio;
using System.Collections.Generic;
using RVModules.RVUtilities;
using System.Collections;

namespace RVExt
{
    public class UseableObject : MonoBehaviour, IScannable, IUseable, IUseableRelationship //, ITarget, IRelationship //, IDamageable, ITargetProvider, IHitPoints
    {
        public bool canUse = true;

        [Tooltip("Don't set too small to avoid overshoot")]
        [SerializeField]
        private float _radius = .2f;

        /*        [SerializeField]
                private AiGroup group;

                [SerializeField]
                private RelationshipSystem _relationshipSystem;
        */

        [SerializeField]
        private Transform _aimTransform;

        [Tooltip("Location to use object from. If unset default is object transform")]
        [SerializeField]
        private Transform _useTransform = null; 

        [SerializeField]
        private UseableRelationshipSystem _useableRelationshipSystem;

        [SerializeField]
        private AiUseableGroup _aiUseableGroup;

        [Tooltip("The time period that this usable will not be visible after use")]
        [SerializeField]
        private float _hideTime = 60f;

        private float _minReuseTime = 10f; // how long Use will ignore repeated calls from same object.

        private Dictionary<GameObject, float> _users = new Dictionary<GameObject, float>();

        public UseableRelationshipSystem UseableRelationshipSystem => _useableRelationshipSystem;

        private void Start()
        {
            StartCoroutine(CleanUsers());
        }

        private IEnumerator CleanUsers()
        {
            while (true)
            {
                yield return new WaitForSeconds(10); // a long time, this is only so the _users list does not grow too big

                float time = UnityTime.Time;
                Dictionary<GameObject, float> toKeep = new Dictionary<GameObject, float>();
                foreach (KeyValuePair<GameObject, float> kvp in _users)
                {
                    if (kvp.Value > UnityTime.Time)
                        toKeep.Add(kvp.Key, kvp.Value);
                }
                _users = toKeep;
            }
        }

        //ITarget
        public float Radius => _radius;

        public Transform Transform => transform;

        //       public Transform AimTransform => _aimTransform ?? transform;

        //public Transform UseTransform => _useTransform ?? transform;
        public Transform UseTransform {
            get { if (_useTransform == null)
                    _useTransform = transform;
                return _useTransform;
                }
            set { _useTransform = value; }
        }
        public float Danger => 0f;

        public Transform VisibilityCheckTransform => transform;

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
        /// <summary>
        /// Returns true if waitTime is still active.
        /// </summary>
        private bool CheckWaitTime(GameObject gameObject, float waitTime)
        {
            if (!_users.ContainsKey(gameObject))
                return false;
            return _users[gameObject] > UnityTime.Time;
        }

        public bool CanUse(GameObject gameObject)
        {
            canUse = !CheckWaitTime(gameObject, _hideTime);

            return canUse;
        }

        public virtual void Use(GameObject gameObject)
        {
            if (CheckWaitTime(gameObject, _minReuseTime))
                return;
            _users.Add(gameObject, UnityTime.Time + (_hideTime < _minReuseTime ? _minReuseTime : _hideTime));
        }

        public bool IsUseable(IUseableRelationship _other) => _useableRelationshipSystem.IsUseable(this, _other);


        //IRelationship
        //public bool IsAlly(IRelationship _other) => _relationshipSystem.IsAlly(this, _other);

        //public bool IsEnemy(IRelationship _other, bool _contraCheck = false) => _relationshipSystem.IsEnemy(this, _other, _contraCheck);


    }
}
