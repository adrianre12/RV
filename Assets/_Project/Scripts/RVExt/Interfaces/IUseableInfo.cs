using System;
using RVHonorAI;
using RVModules.RVSmartAI;
using RVModules.RVUtilities;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RVExt
{
    /// <summary>
    /// Used to store informations about useable like last seen position, is it currently visible etc
    /// </summary>
    [Serializable]
    public class UseableInfo : IPoolable, IUnityComponent, IConvertibleParam<Vector3>
    {
        #region Fields

        /// <summary>
        /// useable cast on Unity object, for debug(inspector) only, editor only(wont be set in build)
        /// </summary>
        [SerializeField]
        private Object useableObject;

        [SerializeField]
        private Vector3 lastSeenPosition;

        [SerializeField]
        private float lastSeenTime;

        [SerializeField]
        private bool visible;

        private IUseable useable;

        #endregion

        #region Properties

        public Action OnSpawn { get; set; }
        public Action OnDespawn { get; set; }

        public IUseable Useable
        {
            get => useable;
            set
            {
                useable = value;
#if UNITY_EDITOR
                useableObject = useable.Object();
#endif
            }
        }

        public Vector3 LastSeenPosition
        {
            get => lastSeenPosition;
            set => lastSeenPosition = value;
        }

        public float LastSeenTime
        {
            get => lastSeenTime;
            set => lastSeenTime = value;
        }

        public bool Visible
        {
            get => visible;
            set => visible = value;
        }

        public Type MyType { get; }

        #endregion

        public UseableInfo()
        {
            MyType = GetType();
            OnSpawn += () =>
            {
                Useable = null;
#if UNITY_EDITOR
                useableObject = null;
#endif
                LastSeenPosition = Vector3.zero;
                LastSeenTime = 0;
                Visible = false;
            };
        }

        /// <summary>
        /// Constructor for new IUseable
        /// </summary>
        /// <param name="_useable">Useable</param>
        /// <param name="_lastSeenPosition">If left unchanged, will be set to useable's transform position</param>
        /// <param name="_visible">Should useable be immediately visible</param>
        public UseableInfo(IUseable _useable, Vector3 _lastSeenPosition = default, bool _visible = true)
        {
            lastSeenPosition = _lastSeenPosition;
            if (lastSeenPosition == default) lastSeenPosition = _useable.Transform.position;
            visible = _visible;
            useable = _useable;
        }

        #region Public methods

        public Component ToUnityComponent() => useableObject as Component;

        public static implicit operator Vector3(UseableInfo _useableInfo) => _useableInfo.LastSeenPosition;

        public Vector3 Convert() => Useable.Transform.position;

        #endregion
    }
}