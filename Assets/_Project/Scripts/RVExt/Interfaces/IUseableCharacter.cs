
using UnityEngine;

namespace RVExt
{
    public interface IUseableCharacter: IUseableInfosProvider, IUseableProvider, IUseableRelationship, IUseablesDetectionCallbacks
    {
        public float UseableMemory { get; }

        public GameObject MyGameObject { get; }
    }
}
