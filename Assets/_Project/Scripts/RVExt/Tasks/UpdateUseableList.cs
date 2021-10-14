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

        private IUseableInfosProvider useableInfoProvider;
        private INearbyObjectsProvider nearbyObjectsProvider;
        private IUseableRelationship ourCharacter;
        public static ObjectPool<UseableInfo> useableInfoPool = new ObjectPool<UseableInfo>(() => new UseableInfo());
        private IUseablesDetectionCallbacks useablesDetectionCallbacks;

        private List<IUseable> newUseables = new List<IUseable>();

        [Tooltip("How long should UseableInfo be kept in IUseableInfosProvider.UseableInfos list after not being seen, in seconds")]
        [SerializeField]
        private FloatProvider useableNotSeenMemorySpan;

        private bool hasUseableDetectionCallbacks;

        private List<UseableInfo> tisToRemove = new List<UseableInfo>();

        #endregion

        #region Not public methods

        protected override void OnContextUpdated()
        {
            nearbyObjectsProvider = Context as INearbyObjectsProvider;
            useableInfoProvider = ContextAs<IUseableInfosProvider>();
            ourCharacter = ContextAs<IUseableRelationship>();
            useablesDetectionCallbacks = GetComponentFromContext<IUseablesDetectionCallbacks>();
            hasUseableDetectionCallbacks = useablesDetectionCallbacks != null;
        }

        protected override void Execute(float _deltaTime)
        {
            var useableInfos = useableInfoProvider.UseableInfosDict;

            var time = UnityTime.Time;

            // get all useables not known earlier earlier
            newUseables.Clear();

            // find all useables that are enemies
            foreach (var o in nearbyObjectsProvider.NearbyObjects)
            {
                if (o == null) continue;

                var useable = o as IUseable;
                if (useable == null) continue;

                // already seen him
                if (useableInfos.ContainsKey(useable))
                {
                    var useableInfo = useableInfos[useable];
                    useableInfo.LastSeenTime = time;
                    useableInfo.LastSeenPosition = useable.Transform.position;
                    if (useableInfo.Visible == false)
                    {
                        useableInfo.Visible = true;
                        if (hasUseableDetectionCallbacks) useablesDetectionCallbacks.OnUseableVisibleAgain?.Invoke(useable);
                    }

                    continue;
                }

                var useableRelationshipProvider = useable as IUseableRelationship;
                if (useableRelationshipProvider == null) continue;

                if (ourCharacter.IsUseable(useableRelationshipProvider))
                {
                    newUseables.Add(useable);
                    if (hasUseableDetectionCallbacks) useablesDetectionCallbacks.OnNewUseableDetected?.Invoke(useable);
                }
            }

            foreach (var useable in newUseables)
            {
                UseableInfo useableInfo = null;

                useableInfo = useableInfoPool.GetObject();
                useableInfo.Useable = useable;
                useableInfos.Add(useableInfo.Useable, useableInfo);
                useableInfoProvider.UseableInfos.Add(useableInfo);

                useableInfo.LastSeenTime = time;
                useableInfo.LastSeenPosition = useable.Transform.position;
                useableInfo.Visible = true;
            }

            foreach (var kvp in useableInfos)
            {
                var useableInfo = kvp.Value;

                if (!nearbyObjectsProvider.NearbyObjects.Contains(useableInfo.Useable as Object) && useableInfo.Visible)
                {
                    useableInfo.Visible = false;
                    if (hasUseableDetectionCallbacks) useablesDetectionCallbacks.OnUseableNotSeenAnymore?.Invoke(kvp.Key);
                }

                if (time > useableInfo.LastSeenTime + useableNotSeenMemorySpan || useableInfo.Useable as Object == null)
                {
                    tisToRemove.Add(useableInfo);
                    if (hasUseableDetectionCallbacks) useablesDetectionCallbacks.OnUseableForget?.Invoke(kvp.Key);
                }
            }

            foreach (var useableInfo in tisToRemove)
            {
                useableInfos.Remove(useableInfo.Useable);
                useableInfoProvider.UseableInfos.Remove(useableInfo);
                useableInfo.OnDespawn();
            }

            tisToRemove.Clear();
        }

        #endregion
    }
}