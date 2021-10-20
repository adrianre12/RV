using RVHonorAI;
using RVModules.RVSmartAI;
using UnityEngine;
using UnityEngine.Events;

namespace RVExt
{
    public interface IUseable : IComponent, IVisibilityCheckTransformProvider, IHitPoints
    {
        #region Properties

        /// <summary>
        /// Useable's radius, used to calculate distance
        /// </summary>
        float UseRadius { get; }

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
        public bool Use(GameObject gameObject);

        /// <summary>
        /// Heal the useable
        /// </summary>
        public bool Heal(float ammount);

        /// <summary>
        /// ratio of durability durability/maxDurability
        /// </summary>
        /// <returns></returns>
        public float DurabilityRatio();

        /// <summary>
        /// UnityEvent called when character dies
        /// </summary>
        UnityEvent OnKilled { get; set; }

    }
}