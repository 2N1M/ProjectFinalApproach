using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GXPEngine.PhysicsEngine
{
    public class Collider
	{
        internal Entity owner;
        SoundChannel collisionSoundChannel;

        public bool IsAttached { get; set; } = false;

        public Collider(Entity owner)
		{
            this.owner = owner;
		}

        public virtual bool HitTest(Collider other)
        {
            return false;
        }
        public virtual bool HitTestPoint(float x, float y)
        {
            return false;
        }

        public virtual List<CollisionInfo> CheckCollisions()
        {
            return EntityManager.Instance
                .GetEntities()
                .Where(other => other != owner && !other.collider.IsAttached) 
                .Select(CheckCollision)
                .Where(collision => collision != null)
                .ToList();
        }

        internal virtual CollisionInfo CheckCollision(Entity other)
        {
            return null;
        }

        internal virtual CollisionInfo SetCollisionTimeOfImpact(CollisionInfo collision)
        {
            return null;
        }
        public CollisionInfo FindEarliestCollision()
        {
            List<CollisionInfo> collisions = CheckCollisions();
            collisions.OrderBy(collision => collision.timeOfImpact).ToList();

            if (collisions.Count > 0)
                return collisions.First();
            else
                return null;
        }

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


        Vec2 RelativeVelocity(Entity otherEntity) => owner.Velocity - otherEntity.Velocity;
        Vec2 RelativePosition(Entity otherEntity) => otherEntity.Position - owner.oldPosition;
        bool ActiveCollision(CollisionInfo collision)
        {
            if (collision.other is Entity other)
            {
                float dot = RelativePosition(other).Dot(RelativeVelocity(other));
                if (dot < 0) // TODO: Check if dot is smaller than 90 deg == more than 0
                    return false;
                return true;
            }
            else
                return true;
        }

        internal virtual bool ResolveCollision(CollisionInfo collision)
        {
            Entity other = collision.other;

            if (this.IsAttached || other.collider.IsAttached || owner.childRigidBodies.Contains(other))
                return false;

            if (!ActiveCollision(collision)) // Check relative velocity to prevent balls that are not on a collision course from colliding
                return false;

            POICollisionResolve(collision);
            Bounce(collision);
            //CollisionSound(collision);

            return true;
        }        
        internal virtual void ResolveCollision(List<CollisionInfo> collision)
        {
            return;
        }

        void POICollisionResolve(CollisionInfo collision)
        {
            owner.Position = CalculatePOI(collision.timeOfImpact);
        }
        public Vec2 CalculatePOI(float TOI)
        {
            return owner.oldPosition + TOI * owner.Velocity;
        }

        Vec2 GetCoMVelocity(CollisionInfo collision)
        {
            if (collision.other.collider is LineCollider)
            {
                return Vec2.Zero;
            }
            else
            {
                Entity other = collision.other;
                float totalMass = owner.Mass + other.Mass;

                Vec2 comVelocity = (owner.GetMomentum() + other.GetMomentum()) / totalMass;

                return comVelocity;
            }
        }
        

        void Bounce(CollisionInfo collision)
        {
            Vec2 comVelocity = GetCoMVelocity(collision); // Velocity of center of mass (weighted average of velocities)
            Vec2 normal = collision.normal;

            if (!collision.other.IsStatic)
            {
                Entity other = collision.other;
                CollisionOnLine(normal, comVelocity);
                other.collider.CollisionOnLine(normal, comVelocity);
            }
            else
            {
                CollisionOnLine(normal, comVelocity);
            }
        }

        void CollisionOnLine(Vec2 normal, Vec2 comVelocity)
        {
            Vec2 projectedVelocity = owner.Velocity.Project(normal);
            Vec2 projectedCoMVelocity = comVelocity.Project(normal);

            owner.Velocity -= (1 + owner.bounciness) * (projectedVelocity - projectedCoMVelocity);
        }

        void CollisionSound(CollisionInfo collision)
        {
            //if (collision.other is LineSegment)
            //    collisionSoundChannel = lineSegmentHit.Play(volume: Mathf.Clamp(1f * velocity.Length, 0.2f, 10f));
            //else if (collision.other is Ball)
            //    collisionSoundChannel = ballHit.Play(volume: 0.8f * velocity.Length);

            collisionSoundChannel.Frequency = Utils.Random(41000, 49000);
        }
    } 
}
