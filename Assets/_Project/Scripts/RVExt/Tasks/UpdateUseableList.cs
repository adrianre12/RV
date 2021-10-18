using System.Collections.Generic;
using RVHonorAI;
using RVModules.RVSmartAI.Content.AI.Contexts;
using RVModules.RVSmartAI.Content.AI.DataProviders;
using RVModules.RVSmartAI.GraphElements;
using RVModules.RVUtilities;
using UnityEngine;

namespace RVExt
{
    /// <summary>
    /// Updates useable and useableInfo lists from IUseableListProvider and IUseableInfosProvider
    /// </summary>
    public class UpdateUseableList : AiTask
    {
        #region Fields

        private INearbyObjectsProvider nearbyObjectsProvider;
        private IUseableCharacter _useableCharacter;
        public static ObjectPool<UseableInfo> useableInfoPool = new ObjectPool<UseableInfo>(() => new UseableInfo());

        private List<IUseable> newUseables = new List<IUseable>();

        [Tooltip("How long should UseableInfo be kept in IUseableInfosProvider.UseableInfos list after not being seen, in seconds")]
        [SerializeField]
        private FloatProvider useableNotSeenMemorySpan;

        private List<UseableInfo> tisToRemove = new List<UseableInfo>();

        #endregion

        #region Not public methods

        protected override void OnContextUpdated()
        {
            nearbyObjectsProvider = Context as INearbyObjectsProvider;
            _useableCharacter = GetComponentFromContext<IUseableCharacter>();
        }

        protected override void Execute(float _deltaTime)
        {
            var useableInfos = _useableCharacter.UseableInfosDict;

            var time = UnityTime.Time;

            // get all useables not known earlier earlier
            newUseables.Clear();

            // find all useables that are enemies
            foreach (var o in nearbyObjectsProvider.NearbyObjects)
            {
                if (o == null) continue;

                var useable = o as IUseable;
                if (useable == null) continue;

                if (!useable.CanUse(this.gameObject))
                    continue;

                // already seen him
                if (useableInfos.ContainsKey(useable))
                {
                    var useableInfo = useableInfos[useable];
                    useableInfo.LastSeenTime = time;
                    useableInfo.LastSeenPosition = useable.UseTransform.position;
                    if (useableInfo.Visible == false)
                    {
                        useableInfo.Visible = true;
                        _useableCharacter.OnUseableVisibleAgain?.Invoke(useable);
                    }

                    continue;
                }

                var useableRelationshipProvider = useable as IUseableRelationship;
                if (useableRelationshipProvider == null) continue;

                if (_useableCharacter.IsUseable(useableRelationshipProvider))
                {
                    newUseables.Add(useable);
                    _useableCharacter.OnNewUseableDetected?.Invoke(useable);
                }
            }

            foreach (var useable in newUseables)
            {
                UseableInfo useableInfo;

                useableInfo = useableInfoPool.GetObject();
                useableInfo.Useable = useable;
                useableInfos.Add(useableInfo.Useable, useableInfo);
                _useableCharacter.UseableInfos.Add(useableInfo);

                useableInfo.LastSeenTime = time;
                useableInfo.LastSeenPosition = useable.UseTransform.position;
                useableInfo.Visible = true;
            }

            foreach (var kvp in useableInfos)
            {
                var useableInfo = kvp.Value;

                if (!nearbyObjectsProvider.NearbyObjects.Contains(useableInfo.Useable as Object) && useableInfo.Visible)
                {
                    useableInfo.Visible = false;
                    _useableCharacter.OnUseableNotSeenAnymore?.Invoke(kvp.Key);
                }

                if (time > useableInfo.LastSeenTime + useableNotSeenMemorySpan || useableInfo.Useable as Object == null || !useableInfo.Useable.CanUse(_useableCharacter.MyGameObject))
                {
                    tisToRemove.Add(useableInfo);
                    _useableCharacter.OnUseableForget?.Invoke(kvp.Key);
                }
            }

            foreach (var useableInfo in tisToRemove)
            {
                useableInfos.Remove(useableInfo.Useable);
                _useableCharacter.UseableInfos.Remove(useableInfo);
                useableInfo.OnDespawn();
            }

            tisToRemove.Clear();
        }

        #endregion
    }
}