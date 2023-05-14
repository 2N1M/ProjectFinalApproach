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
        const float gravitationalConstant = 1e-3f;
        float gravitationalForce;

        public GravityArea(Entity owner) : base(owner)
        {
            IsAttached = true;
        }

        internal override CollisionInfo CheckCollision(Entity other)
        {
            Vec2 difference = owner.oldPosition - other.Position;

            if (HitTest(other.collider) && other != owner.parentRigidBody)
                return new CollisionInfo(owner, other, difference.Normalized(), difference);
            return null;
        }

        internal override void ResolveCollision(List<CollisionInfo> collisions)
        {
            foreach (var collision in collisions)
            {
                if (collision.other.collider is CircleCollider && collision.other != owner.parentRigidBody && collision.other.collider.GetType() != typeof(GravityArea))
                {
                    Entity other = collision.other;

                    float distance = Vec2.Distance(collision.other.Position, owner.Position);
                    other.gravityForces.Add(GravityTowardsCenter(other, distance));
                }
            }
        }

        Vec2 GravityTowardsCenter(Entity other, float distance)
        {
            Vec2 gravityDirection = (owner.Position - other.Position).Normalized();
            float temp = gravitationalConstant * owner.parentRigidBody.Mass * other.Mass;
            gravitationalForce = (float)temp / Mathf.Pow(distance,2);
            return gravityDirection * gravitationalForce;// Mathf.Clamp(gravitationalForce, 0, 0.5f);
        }
    }
}
