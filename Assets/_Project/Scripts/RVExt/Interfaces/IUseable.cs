using RVHonorAI;
using RVModules.RVSmartAI;
using UnityEngine;
 

namespace RVExt
{
    public interface IUseable : IComponent, IVisibilityCheckTransformProvider
    {
        #region Properties

        /// <summary>
        /// Useable's radius, used to calculate distance
        /// </summary>
        float Radius { get; }

        /// <summary>
        /// Useable's main transform, for checking position, rotation etc.. usually root transform of entity
        /// </summary>
        Transform Transform { get; }

        /// <summary>
        /// Transform for use position.
        /// </summary>
        Transform UseTransform { get; }

        #endregion


        /// <summary>
        /// Check to see if the gameObject can use this
        /// </summary>
        public bool CanUse(GameObject gameObject);

        /// <summary>
        /// Use this object
        /// </summary>
        public void Use(GameObject gameObject);
        
    }
}