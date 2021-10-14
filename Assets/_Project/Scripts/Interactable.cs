using UnityEngine;
using RVModules.RVSmartAI.Content.Scanners;
using RVHonorAI;
using RVHonorAI.Combat;
using RVHonorAI.Systems;
using RVModules.RVCommonGameLibrary.Audio;

public class Interactable : MonoBehaviour, ITarget, IScannable, IRelationship//, IDamageable, ITargetProvider, IHitPoints
{
    private float _radius = .5f;

    [SerializeField]
    private AiGroup group;

    [SerializeField]
    private Transform _aimTransform;

    [SerializeField]
    private RelationshipSystem _relationshipSystem;

    //ITarget
    public float Radius => _radius;

    public Transform Transform => transform;

    public Transform AimTransform => _aimTransform;

    public float Danger => 0f;

    public Transform VisibilityCheckTransform => _aimTransform;

    //public AiGroup AiGroup { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    AiGroup IRelationship.AiGroup
    {
        get { return group; }
        set { }
    }
    public bool TreatNeutralCharactersAsEnemies => false;


    //IRelationship
    public bool IsAlly(IRelationship _other) => _relationshipSystem.IsAlly(this, _other);

    public bool IsEnemy(IRelationship _other, bool _contraCheck = false) => _relationshipSystem.IsEnemy(this, _other, _contraCheck);


}

