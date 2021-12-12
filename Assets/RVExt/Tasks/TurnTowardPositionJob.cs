using System;
using RVHonorAI;
using RVModules.RVSmartAI.Content.AI.DataProviders;
using RVModules.RVSmartAI.Content.AI.Tasks;
using RVModules.RVUtilities;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RVExt
{
    /// <summary>
    /// Rrotate ai agent toward a position using simple transform manipulation and by using root motion
    /// </summary>
    public class TurnTowardPosition : AiJob
    {
        [SerializeField]
        private Vector3Provider _position;

        private float currentRotationSpeed;

        private ICharacterAnimation characterAnimation;

        private float timeInRightAngle;
        private float timeInRightAngleToFinishAiming = 2;

        private bool charMoving;
        private float lastTimeCharMovingChanged;

        protected override string DefaultDescription => "";


        private void OnEnable() => name = "TurnTowardPosition";

        protected override void OnContextUpdated()
        {
            base.OnContextUpdated();
            characterAnimation = ContextAs<ICharacterAnimationProvider>().CharacterAnimation;
        }

        protected override void OnJobUpdate(float _dt)
        {
            var myTransform = movement.Transform;
            var myPosition = myTransform.position;
            myPosition.y = 0;
            Vector3 position = _position.GetData();
            position.y = 0;

            var angle = Vector3.SignedAngle(myTransform.forward, position - myPosition, Vector3.up);

            float deadZone = 6;
            float targetRotationSpeed = Mathf.Lerp(0, 1, Math.Abs(angle) * .035f);

            if (angle < 0) targetRotationSpeed *= -1;

            currentRotationSpeed = Mathf.Lerp(currentRotationSpeed, targetRotationSpeed, UnityTime.DeltaTime * 5);

            var rotSpeed = currentRotationSpeed * movement.RotationSpeed * UnityTime.DeltaTime;

            var moving = movement.Velocity.sqrMagnitude > 1;

            // allow to change 'charMoving' only one time threshold to avoid agressive constant animation transistions that interfere with attacks animations  
            if (moving != charMoving && UnityTime.Time - lastTimeCharMovingChanged > .5f)
            {
                charMoving = moving;
                lastTimeCharMovingChanged = UnityTime.Time;
            }

            characterAnimation.Moving = charMoving;
            var inRightAngle = Math.Abs(angle) < deadZone;
            characterAnimation.Rotating = !charMoving;

            // for animation we want normalized rotation speed value!
            characterAnimation.RotatingSpeed = currentRotationSpeed;

            // we take control over rotation of our character to allow facing other direction for aiming when moving
            if (!characterAnimation.UseRootMotion || charMoving) myTransform.Rotate(Vector3.up, rotSpeed, Space.Self);

            if (inRightAngle)
            {
                timeInRightAngle += UnityTime.DeltaTime;
                if (timeInRightAngle > timeInRightAngleToFinishAiming) FinishJob();
            }
            else
            {
                timeInRightAngle = 0;
            }
        }

        protected override void OnJobStart()
        {
            timeInRightAngle = 0;
            movement.UpdateRotation = false;
            charMoving = false;
            lastTimeCharMovingChanged = 0;
        }

        protected override void OnJobFinish()
        {
            characterAnimation.Rotating = false;
            characterAnimation.Moving = false;
            characterAnimation.RotatingSpeed = 0;

            if (Context as Object == null) return;
            movement.UpdateRotation = true;
        }
    }
}
