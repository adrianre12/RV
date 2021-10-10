// Original created by Ronis Vision. All rights reserved
// 03.07.2021.

using System;
using RVHonorAI;
using RVHonorAI.Combat;
using RVHonorAI.Systems;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

public class PlayerCharacter : MonoBehaviour, ICharacter
{
    /// <summary>
    /// This is called by the NPC to apply damage to the player. The return value should be the actuall damage done to the player after all protections (buffs, equipment etc) have been applied. 
    /// </summary>
    public float ReceiveDamage(float _damage, Object _damageSource, DamageType _damageType, bool _damageEnemyOnly = false, Vector3 hitPoint = default, Vector3 _hitForce = default, float forceRadius = default)
    {
        Debug.LogFormat("Damage recieved {0}", _damage);
        return _damage;
    }
        

    #region Fields

    [SerializeField]
    private AiGroup group;

    [SerializeField]
    private Transform headTransform;

    private float radius = .5f;

    [SerializeField]
    private RelationshipSystem relationshipSystem;

    #endregion

    #region Properties

    //        public Object GetObject => this;

    public float Danger => .1f * HitPoints * .1f * DamagePerSecond;
    public float MaxHitPoints { get; }
    public UnityEvent OnKilled { get; set; }
    public float RunningSpeed { get; set; }
    public float WalkingSpeed { get; set; }
    public IAttack CurrentAttack { get; set; }
    public IWeapon CurrentWeapon { get; }
    public ICharacterAi CharacterAi { get; }

    public float Radius => radius;
    public Transform Transform => transform;
    public Transform AimTransform => headTransform;
    public Transform HeadTransform => headTransform;

    public float HitPoints { get; set; } = 100;

//        public AiGroup AiGroup => group;
    public bool TreatNeutralCharactersAsEnemies { get; }

    public float DamagePerSecond => 40;

    public float Armor => 20;

    public float Courage => 50;

    public float AttackRange => 2;

    //AiGroup IRelationship.AiGroup { get; set; }
    AiGroup IRelationship.AiGroup
    {
        get { return group; }
        set { }
    }
    
    public Transform VisibilityCheckTransform => HeadTransform;
    public Vector3 FovPosition => HeadTransform.position;
    public ITarget Target { get; }
    public TargetInfo CurrentTarget { get; set; }

    #endregion

    #region Public methods

    public void Kill() => throw new NotImplementedException();

    public void Kill(Vector3 hitPoint, Vector3 hitForce = default, float forceRadius = default) => throw new NotImplementedException();

    public void Heal(float _amount)
    {
    }

    public virtual bool IsEnemy(IRelationship _other, bool _contraCheck = false) => relationshipSystem.IsEnemy(this, _other, _contraCheck);

    public virtual bool IsAlly(IRelationship _other) => relationshipSystem.IsAlly(this, _other);

    #endregion
}
