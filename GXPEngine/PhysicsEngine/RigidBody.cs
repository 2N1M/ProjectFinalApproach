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

        public bool IsStatic { get; set; } = false;

        public float bounciness = 0.58f;
        public static Vec2 gravityAcceleration = new Vec2(0, 0);        
        public float surfaceFrictionCoefficient = 0.05f;
        public float airFrictionCoefficient = 0.001f;

        public List<Vec2> gravityForces= new List<Vec2>();

        internal Vec2 movementAccelaration = new Vec2(0, 0);
        public Vec2 hitAccelaration;

        public Vec2 oldPosition;
        Arrow velocityIndicator;
        Arrow gravityIndicator;

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
                        return Mathf.PI * Mathf.Pow(radius,2) * Density;
                    default:
                        return width * height * Density;
                }
            }
        }

        public RigidBody(Texture2D spriteSheet, int cols = 1, int rows = 1, int frames = -1) : base(spriteSheet, cols, rows, frames)
        {
            velocityIndicator = new Arrow(Vec2.Zero, Vec2.Zero, 10);
            gravityIndicator = new Arrow(Vec2.Zero, Vec2.Zero, 0.001f, 0xff50ff50);
            childRigidBodies = new List<RigidBody>();

            LateAddChild(velocityIndicator);
            LateAddChild(gravityIndicator);
            SetOriginCenter();
        }

        public void Step()
        {
            oldPosition = Position;

            if (!IsStatic)
            {
                gravityIndicator.startPoint = Position;
                gravityIndicator.vector = gravityForces.Aggregate(Vec2.Zero, (acc, vec) => acc + vec);
                Move();
            }
            
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
            if (collider.IsAttached)
            {
                collider.ResolveCollision(GetCollisions());
                if (parentRigidBody != null)
                    Position = parentRigidBody.Position;
                return;
            }

            Vec2 gravityForceSum = gravityForces.Aggregate(Vec2.Zero, (acc, vec) => acc + vec);
            
            gravityAcceleration = (gravityForceSum / Mass);
            Velocity += gravityAcceleration + movementAccelaration + hitAccelaration;
            hitAccelaration = Vec2.Zero;
            gravityForces.Clear();

            int maxIterations = 2;
            int iterationCount = 0;
            while (iterationCount < maxIterations)
            {
                iterationCount++;

                Position += Velocity;

                CollisionInfo collision = FindEarliestCollision();
                if (collision != null && collider.ResolveCollision(collision))
                {
                    Vec2 normalForce = gravityForceSum.Project(collision.normal);
                    Vec2 friction = Velocity.Project(Velocity.PerpendicularUnit(collision.normal)) * collision.other.surfaceFrictionCoefficient;

                    Velocity -= ((normalForce / Mass) + friction);

                    if (Vec2.Approximately(collision.timeOfImpact, 0) || normalForce != Vec2.Zero)
                        continue;
                    else
                        break;
                }
                break;
            }
        }

        CollisionInfo FindEarliestCollision()
        {
            return collider.FindEarliestCollision();
        }
        List<CollisionInfo> GetCollisions()
        {
            return collider.CheckCollisions();
        }

        public Vec2 GetMomentum()
        {
            return Velocity * Mass;
        }
    }
}
