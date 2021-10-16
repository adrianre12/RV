using RVHonorAI;
using RVHonorAI.CharacterInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RVExt
{
    public class UseableCharacterAi : CharacterAi, IUseableCharacter
    {
        [SerializeField]
        [CharacterInspectorField(drawWhenNotPlaying = false)]
        private List<UseableInfo> _useableInfos;

        private Dictionary<IUseable, UseableInfo> _useableInfosDict = new Dictionary<IUseable, UseableInfo>();

        private float _useableMemory = 20;

        private UseableCharacterSettings settings;

        private UseableInfo currentUseable;


        public List<UseableInfo> UseableInfos => _useableInfos;

        public Dictionary<IUseable, UseableInfo> UseableInfosDict => _useableInfosDict;

        public float UseableMemory => _useableMemory;

        public UseableRelationshipSystem UseableRelationshipSystem => settings.useableRelationshipSystem;

        public AiUseableGroup AiUseableGroup { 
            get => settings.aiUseableGroup;
            set => settings.aiUseableGroup = value; 
        }

        public Action<IUseable> OnNewUseableDetected { get; set; }
        public Action<IUseable> OnUseableNotSeenAnymore { get; set; }
        public Action<IUseable> OnUseableVisibleAgain { get; set; }
        public Action<IUseable> OnUseableForget { get; set; }

        public IUseable Useable => currentUseable?.Useable;

        public UseableInfo CurrentUseable { 
            get => currentUseable;
            set => currentUseable = value; }

        public bool IsUseable(IUseableRelationship _other) => settings.useableRelationshipSystem.IsUseable(this, _other);

        protected override void Awake()
        {
            base.Awake();
            if (!TryGetComponent<UseableCharacterSettings>(out settings))
                Debug.LogWarning("UseableCharacterSettings component not found");
        }
        protected override void SetupGraphVariables()
        {
            base.SetupGraphVariables();
            if (Ai.SecondaryGraphs == null || Ai.SecondaryGraphs.Length == 0)
            {
                Debug.LogWarning("There is no secondary AI graph. It is needed for Useable");
            }
            else
            {
                var scanningGraphVars = Ai.SecondaryGraphs[0].GraphAiVariables;

                scanningGraphVars.AssureFloatExist(nameof(UseableMemory));
                scanningGraphVars.SetFloat(nameof(UseableMemory), UseableMemory);
            }
        }
    }
}
