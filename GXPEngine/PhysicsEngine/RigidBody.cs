using GXPEngine.Core;
using GXPEngine.PhysicsEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.PhysicsEngine
{
    public enum ColliderType
    {
        Box,
        Line,
        Circle,
        Trigger
    }

    public class RigidBody : AnimationSprite
    {
        public new Collider collider;

        public int radius;

        public float bounciness = 0.98f;
        public static Vec2 acceleration = new Vec2(0, 0);
        public float frictionCoefficient = 0.02f;
        private Vec2 friction;
        public Vec2 gravityDirection;
        public Vec2 hitAccelaration;

        public Vec2 oldPosition;
        Arrow velocityIndicator;

        SoundChannel collisionSoundChannel;

        internal bool easyDraw = false;

        public float Mass
        {
            get
            {
                switch (collider)
                {
                    case BoxCollider boxCollider:
                        return width * height * Density;
                    case CircleCollider circleCollider:
                        return radius * radius * Density;
                    default:
                        return width * height * Density;
                }
            }
        }

        public RigidBody(Texture2D spriteSheet, int cols = 1, int rows = 1, int frames = -1) : base(spriteSheet, cols, rows, frames)
        {
            velocityIndicator = new Arrow(Vec2.Zero, Vec2.Zero, 10);
            AddChild(velocityIndicator);
            SetOriginCenter();
        }

        public void Step()
        {
            oldPosition = Position;

            Move();
            if (easyDraw)
                DrawShape();
            ShowDebugInfo();
        }
        internal virtual void DrawShape()
        {
            // Empty
        }
        void ShowDebugInfo()
        {
            velocityIndicator.startPoint = Position;
            velocityIndicator.vector = Velocity;
        }
        void Move() // Movement using Euler integration
        {
            if (this.GetType() != typeof(StaticObject))
            {                
                friction = Velocity * frictionCoefficient;
                Velocity += acceleration + hitAccelaration + gravityDirection - friction;

                frictionCoefficient = 0.02f;
                gravityDirection = Vec2.Zero;

                int maxIterations = 2;
                int iterationCount = 0;

                while (iterationCount < maxIterations)
                {
                    iterationCount++;
                    Position += Velocity;

                    CollisionInfo collision = FindEarliestCollision();
                    gravityDirection = Vec2.Zero; // Set gravity direction to 0 so that gravity from holes doesn't keep affecting ball after "missing" hole
                    if (collision != null)
                    {
                        ResolveCollision(collision);
                        if (!Vec2.Approximately(collision.timeOfImpact, 0))
                            break;
                    }
                }
            }
            else
            {
                CollisionInfo collision = FindEarliestCollision();
                if (collision != null)
                {
                    ResolveCollision(collision);
                }
            }
        }

        CollisionInfo FindEarliestCollision()
        {
            return collider.GetCollisionInfo();
        }

        Vec2 RelativeVelocity(Entity otherEntity) => this.Velocity - otherEntity.Velocity;
        Vec2 RelativePosition(Entity otherEntity) => otherEntity.Position - this.oldPosition;
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

        internal virtual void ResolveCollision(CollisionInfo collision)
        {
            if (collision.self is InteractionObject || collision.other is InteractionObject)
                return;

            if (!ActiveCollision(collision)) // Check relative velocity to prevent balls that are not on a collision course from colliding
                return;

            POICollisionResolve(collision);
            Bounce(collision);
            //CollisionSound(collision);
        }

        void POICollisionResolve(CollisionInfo collision)
        {
            Position = CalculatePOI(collision.timeOfImpact);
        }
        public Vec2 CalculatePOI(float TOI)
        {
            return oldPosition + TOI * Velocity;
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
                float totalMass = this.Mass + other.Mass;

                Vec2 comVelocity = (this.GetMomentum() + other.GetMomentum()) / totalMass;

                return comVelocity;
            }
        }
        Vec2 GetMomentum()
        {
            return Velocity * Mass;
        }

        void Bounce(CollisionInfo collision)
        {
            Vec2 comVelocity = GetCoMVelocity(collision); // Velocity of center of mass (weighted average of velocities)
            Vec2 normal = collision.normal;

            if (collision.other.GetType() != typeof(StaticObject))
            {
                Entity other = collision.other;
                CollisionOnLine(normal, comVelocity);
                other.CollisionOnLine(normal, comVelocity);
            }
            else
            {
                CollisionOnLine(normal, comVelocity);
            }
        }

        void CollisionOnLine(Vec2 normal, Vec2 comVelocity)
        {
            Vec2 projectedVelocity = Velocity.Project(normal);
            Vec2 projectedCoMVelocity = comVelocity.Project(normal);
            
            Velocity = Velocity - (1 + bounciness) * (projectedVelocity - projectedCoMVelocity);
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
