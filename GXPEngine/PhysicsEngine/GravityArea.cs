using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.PhysicsEngine
{
    internal class GravityArea : CircleCollider
    {
        float gravitationalConstant = 800;
        float gravitationalForce;

        public GravityArea(Entity owner) : base(owner)
        {
            IsStatic = true;
        }

        internal override CollisionInfo CheckCollision(Entity other)
        {
            Vec2 difference = owner.oldPosition - other.Position;

            if (HitTest(other.collider) && other != owner.parentRigidBody)
                return new CollisionInfo(owner, other, difference.Normalized(), difference);
            return null;
        }

        internal override void ResolveCollision(CollisionInfo collision)
        {
            
            if (collision.other.collider is CircleCollider && collision.other != owner.parentRigidBody)
            {                
                Entity other = collision.other;
                
                float distance = Vec2.Distance(collision.other.Position, owner.Position);

                //other.frictionCoefficient = 0.07f;
                other.gravityDirection = GravityTowardsCenter(other, distance);

                //if (other.Velocity.Length < 2 && distance < 1 && gravitationalForce > 1) // If ball has basically come to a standstill
                //{
                //    other.Velocity = Vec2.Zero;
                //}
                //else //if (distance < owner.radius) // Only if distance is smaller then the radius of the ball should the ball "fall in"
                //{
                //    other.frictionCoefficient = 0.07f;
                //    other.gravityDirection = GravityTowardsCenter(other, distance);
                //}
            }
        }

        Vec2 GravityTowardsCenter(Entity entity, float distance)
        {
            Vec2 gravityDirection = (owner.Position - entity.Position).Normalized();
            gravitationalForce = gravitationalConstant / (distance * distance);
            return gravityDirection * gravitationalForce;// Mathf.Clamp(gravitationalForce, 0, 0.5f);
        }
    }
}
