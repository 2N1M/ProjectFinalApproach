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
        InsideCircle,
        GravityArea,
        Trigger
    }

    public class RigidBody : AnimationSprite
    {
        public RigidBody parentRigidBody = null;
        public List<RigidBody> childRigidBodies = null;

        public new Collider collider;

        public static bool drawDebugLine = false;

        public int radius;

        public float bounciness = 0.98f;
        public static Vec2 acceleration = new Vec2(0, 0);
        public float frictionCoefficient = 0.02f;
        private Vec2 friction;
        public Vec2 gravityDirection;
        public Vec2 hitAccelaration;

        public Vec2 oldPosition;
        Arrow velocityIndicator;

        internal bool easyDraw = false;

        public float Mass
        {
            get
            {
                switch (collider)
                {
                    case BoxCollider _:
                        return width * height * Density;
                    case CircleCollider _:
                        return radius * radius * Density;
                    default:
                        return width * height * Density;
                }
            }
        }

        public RigidBody(Texture2D spriteSheet, int cols = 1, int rows = 1, int frames = -1) : base(spriteSheet, cols, rows, frames)
        {
            velocityIndicator = new Arrow(Vec2.Zero, Vec2.Zero, 10);
            childRigidBodies= new List<RigidBody>();
            LateAddChild(velocityIndicator);
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

            if (drawDebugLine)
            {
                ((MyGame)game).DrawLine(oldPosition, Position);
            }
        }
        void Move() // Movement using Euler integration
        {
            if (collider.IsStatic)
            {
                CollisionInfo collision = FindEarliestCollision();
                if (collision != null)
                {
                    collider.ResolveCollision(collision);
                }

                if (parentRigidBody != null)
                {
                    Position = parentRigidBody.Position;                    
                }
                return;
            }

            friction = Velocity * frictionCoefficient;
            Velocity += acceleration + hitAccelaration + gravityDirection;// - friction;

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
                    collider.ResolveCollision(collision);
                    if (!Vec2.Approximately(collision.timeOfImpact, 0))
                        break;
                }
            }
        }

        CollisionInfo FindEarliestCollision()
        {
            return collider.FindEarliestCollision();
        }

        public Vec2 GetMomentum()
        {
            return Velocity * Mass;
        }
    }
}
