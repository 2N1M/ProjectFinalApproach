using GXPEngine;
using GXPEngine.Core;
using GXPEngine.PhysicsEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GXPEngine.PhysicsEngine
{
	public class Collider
	{
        internal Entity owner;

        public bool IsTrigger
        {
            get
            {
                return isTrigger;
            }
            set
            {
                isTrigger = value;
            }
        }
        bool isTrigger = false;

        public Collider()
		{
		}

        public virtual bool HitTest(Collider other)
        {
            return false;
        }

        public virtual bool HitTestPoint(float x, float y)
        {
            return false;
        }

        internal virtual List<CollisionInfo> CheckCollisions()
        {
            return null;
        }
        internal virtual CollisionInfo SetTimeOfImpact(CollisionInfo collision)
        {
            return null;
        }
        public CollisionInfo GetCollisionInfo()
        {
            if (owner is InteractionObject && CheckCollisions().Count > 0)
            {
                return CheckCollisions().OrderBy(collision => collision.timeOfImpact).ToList().First();
            }

            List<CollisionInfo> collisions = CheckCollisions();
            collisions.OrderBy(collision => collision.timeOfImpact).ToList();

            if (collisions.Count > 0)
                return collisions.First();
            else
                return null;
        }

        /// <summary>
        /// Determines whether two circles overlap by calculating the distance between their centers and comparing it to the sum of their radii.
        /// </summary>
        /// <param name="c1">The center of the first circle.</param>
        /// <param name="c2">The center of the second circle.</param>
        /// <param name="r1">The radius of the first circle.</param>
        /// <param name="r2">The radius of the second circle.</param>
        /// <returns>True if the two circles overlap, otherwise false.</returns>
        internal bool CircleOverlap(Vec2 c1, Vec2 c2, float r1, float r2)
        {
            return (Vec2.Distance(c1, c2) < r1 + r2);
        }
        internal bool AreaOverlap(Vec2[] c, Vec2[] d)
        {
            // normal 1:
            float ny = c[1].x - c[0].x;
            float nx = c[0].y - c[1].y;
            // own 'depth' in direction of this normal:
            float dx = c[3].x - c[0].x;
            float dy = c[3].y - c[0].y;
            float dot = (dy * ny + dx * nx);

            if (dot == 0.0f) dot = 1.0f;

            float t, minT, maxT;

            t = ((d[0].x - c[0].x) * nx + (d[0].y - c[0].y) * ny) / dot;
            maxT = t; minT = t;

            t = ((d[1].x - c[0].x) * nx + (d[1].y - c[0].y) * ny) / dot;
            minT = Math.Min(minT, t); maxT = Math.Max(maxT, t);

            t = ((d[2].x - c[0].x) * nx + (d[2].y - c[0].y) * ny) / dot;
            minT = Math.Min(minT, t); maxT = Math.Max(maxT, t);

            t = ((d[3].x - c[0].x) * nx + (d[3].y - c[0].y) * ny) / dot;
            minT = Math.Min(minT, t); maxT = Math.Max(maxT, t);

            if ((minT >= 1) || (maxT <= 0)) return false;

            // second normal:
            ny = dx;
            nx = -dy;
            dx = c[1].x - c[0].x;
            dy = c[1].y - c[0].y;
            dot = (dy * ny + dx * nx);

            if (dot == 0.0f) dot = 1.0f;

            t = ((d[0].x - c[0].x) * nx + (d[0].y - c[0].y) * ny) / dot;
            maxT = t; minT = t;

            t = ((d[1].x - c[0].x) * nx + (d[1].y - c[0].y) * ny) / dot;
            minT = Math.Min(minT, t); maxT = Math.Max(maxT, t);

            t = ((d[2].x - c[0].x) * nx + (d[2].y - c[0].y) * ny) / dot;
            minT = Math.Min(minT, t); maxT = Math.Max(maxT, t);

            t = ((d[3].x - c[0].x) * nx + (d[3].y - c[0].y) * ny) / dot;
            minT = Math.Min(minT, t); maxT = Math.Max(maxT, t);

            if ((minT >= 1) || (maxT <= 0)) return false;

            return true;
        }
    } 
}
