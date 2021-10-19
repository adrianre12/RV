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
    public class UseableObject : MonoBehaviour, IScannable, IUseable, IUseableRelationship, ITarget, IRelationship, IDamageable, ITargetProvider, IHitPoints
    {
        #region Useable
        public bool canUse = true;

        [Tooltip("How close should the NPC be to use. Don't set too small to avoid overshoot")]
        [SerializeField]
        private float _useRadius = .2f;

        [Tooltip("Location to use object from. If unset default is object transform")]
        [SerializeField]
        private Transform _useTransform = null; 

        [SerializeField]
        private UseableRelationshipSystem _useableRelationshipSystem;

        [SerializeField]
        private AiUseableGroup _aiUseableGroup;

        [Tooltip("The time period that this usable will not be visible (ignored) after use.")]
        [SerializeField]
        private float _hideTime = 60f;

        private float _minReuseTime = 1f; // how long Use will ignore repeated calls from same object.

        private Dictionary<GameObject, float> _users = new Dictionary<GameObject, float>();

        public UseableRelationshipSystem UseableRelationshipSystem => _useableRelationshipSystem;

        public float UseRadius => _useRadius;

        public Transform UseTransform {
            get { if (_useTransform == null)
                    _useTransform = transform;
                return _useTransform;
                }
            set { _useTransform = value; }
        }

        public Transform VisibilityCheckTransform => transform;

        public AiUseableGroup AiUseableGroup
        {
            get => _aiUseableGroup;
            set => _aiUseableGroup = value;
        }


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

        /// <summary>
        /// Override this to perform use action.
        /// Ensure you call base.Use() first.
        /// To stop rapid repeated use, either distroy this game object or set hideTime to the interval required
        /// </summary>
        public virtual void Use(GameObject gameObject)
        {
            if (CheckWaitTime(gameObject, _minReuseTime))
                return;
            _users.Add(gameObject, UnityTime.Time + (_hideTime < _minReuseTime ? _minReuseTime : _hideTime));
        }


        /// <summary>
        /// Heals set amount of durability
        /// </summary>
        /// <param name="_amount">Amount of hp to add</param>
        public virtual void Heal(float _amount) => HitPoints += Mathf.Clamp(_amount, 0, float.MaxValue);

        public float DurabilityRatio()
        {
            return _durability/_maxDurability;
        }

        #endregion



        #region Attackable 

        [Tooltip("How close should the NPC be to attack. Don't set too small to avoid overshoot")]
        [SerializeField]
        private float _attackRadius = .2f;

        [SerializeField]
        private AiGroup group;

        [SerializeField]
        private RelationshipSystem _relationshipSystem;

        [SerializeField]
        private DamageSystem _damageSystem;

        [SerializeField]
        private Transform _aimTransform;

        [SerializeField]
        private float _durability = 100;

        [SerializeField]
        private float _maxDurability = 100;
        private object aiSystems;

        public float Radius => _attackRadius;

        public float Danger => 0f;

        AiGroup IRelationship.AiGroup
        {
            get { return group; }
            set { }
        }

        public bool TreatNeutralCharactersAsEnemies => false;

        public ITarget Target { get; }

        public TargetInfo CurrentTarget { get; set; }

        public float HitPoints
        {
            get => _durability;
            set => _durability = Mathf.Clamp(value, 0, _maxDurability);
        }

        public float MaxHitPoints
        {
            get => _maxDurability;
            set => _maxDurability = value;
        }

        public Transform Transform => transform;

        public Transform AimTransform => _aimTransform ?? transform;

        public bool IsUseable(IUseableRelationship _other) => _useableRelationshipSystem.IsUseable(this, _other);

        //IRelationship
        public bool IsAlly(IRelationship _other) => _relationshipSystem.IsAlly(this, _other);

        public bool IsEnemy(IRelationship _other, bool _contraCheck = false) => _relationshipSystem.IsEnemy(this, _other, _contraCheck);

        public float ReceiveDamage(float _damage, Object _damageSource, DamageType _damageType, bool _damageEnemyOnly = false, Vector3 _hitPoint = default, Vector3 _hitForce = default, float _forceRadius = 0)
        {
            //if (RandomChance.Get(characterSounds.chanceToPlayGotHitSound))
            //    AudioManager.Instance.PlaySound(transform.position, characterSounds.gotHitSound, AudioSource, true);

            var receivedDmg = _damageSystem.ReceiveDamage(this, _damageSource, _damage, 0f, _damageType, _damageEnemyOnly, _hitPoint, _hitForce, _forceRadius);

            //OnGotHit.Invoke(_damageSource, receivedDmg);
            //if (receivedDmg > 0) OnReceivedDamage?.Invoke(_damageSource, receivedDmg);

            if (_durability <= 0)
            {
                Kill(_hitPoint, _hitForce, _forceRadius);
            }
            return _damage;
        }

        /// <summary>
        /// Instantly kills this useable 
        /// </summary>
        public virtual void Kill() => Kill(Vector3.zero);

        /// <summary>
        /// Instantly kills this useable 
        /// </summary>
        public virtual void Kill(Vector3 _hitPoint, Vector3 _hitForce = default, float _forceRadius = default)
        {
            Destroy(gameObject);
        }



        #endregion
    }
}
