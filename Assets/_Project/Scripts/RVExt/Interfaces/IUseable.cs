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
        /// Transform for aiming at useable, also used for checking fov with raycasts etc...
        /// </summary>
        Transform AimTransform { get; }

        #endregion
    }
}