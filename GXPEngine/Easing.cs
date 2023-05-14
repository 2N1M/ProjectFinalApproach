using GXPEngine.Core;

namespace GXPEngine
{
    public class Easing
    {
        public float easeTarget;
        Vec2 easeTargets;

        /// <summary>
        /// Simple linear interpolation to calculate the eased value
        /// </summary>
        public float Ease(float rawValue, float easing)
        {
            return easeTarget = (rawValue - easeTarget) * easing + easeTarget;
        }

        /// <summary>
        /// Simple linear interpolation to calculate the eased Vec2 values
        /// </summary>
        public Vec2 Ease(Vec2 rawValues, float easing)
        {
            return easeTargets = (rawValues - easeTargets) * easing + easeTargets;
        }

        public float EaseAngle(float fromAngle, float toAngle, float easing)
        {
            // Calculate the angle difference and wrap it around the range of -180 to 180 degrees
            float angleDiff = DeltaAngle(fromAngle, toAngle);

            // Interpolate the angle in the most appropriate direction
            float newAngle = fromAngle + (angleDiff * easing);

            return newAngle;
        }
        float DeltaAngle(float current, float target)
        {
            float delta = target - current;
            if (delta < -180f) delta += 360f;
            if (delta > 180f) delta -= 360f;
            return delta;
        }
    }
}
