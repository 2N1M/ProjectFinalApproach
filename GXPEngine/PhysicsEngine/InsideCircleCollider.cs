using System;
using System.Collections.Generic;
using System.Drawing;
using GXPEngine;
using GXPEngine.Core;
using GXPEngine.PhysicsEngine;

namespace GXPEngine.PhysicsEngine
{
	public class InsideCircleCollider : CircleCollider
	{
        Vec2 colliderPosition;

        public InsideCircleCollider(Entity owner) : base(owner)
		{
            
		}

        //internal override List<CollisionInfo> CheckCollisions()
        //{
        //    List<CollisionInfo> collisions = new List<CollisionInfo>();
        //    CollisionInfo collision;

        //    foreach (var other in EntityManager.Instance.GetEntities())
        //    {
        //        if (other != owner) // Continuous collision detection 
        //        {
        //            Vec2 difference = owner.oldPosition - other.Position;

        //            Vec2 centersDifference = owner.oldPosition - other.Position;
        //            Vec2 centersDiffNormalized = centersDifference.Normalized();
        //            colliderPosition = owner.Position + (centersDiffNormalized * (this.Radius + other.radius));

        //            difference = colliderPosition - other.Position;

        //            //if (other.collider is CircleCollider)
        //            //{
                        
        //            //    Vec2 centersDifference = owner.oldPosition - other.Position;
        //            //    Vec2 centersDiffNormalized = centersDifference.Normalized();
        //            //    colliderPosition = owner.Position + (centersDiffNormalized * (this.Radius + other.radius));

        //            //    difference = colliderPosition - other.Position;
        //            //}

        //            collision = new CollisionInfo(owner, other, Vec2.Zero, difference); // Normal 0 because it still has to be calculated by TOI
        //            CollisionInfo updatedCollision = SetCollisionTimeOfImpact(collision);

        //            if (!(other is InteractionObject) && updatedCollision != null)
        //                collisions.Add(updatedCollision);

        //            //if (HitTest(other.collider))// && owner is InteractionObject)
        //            //    collisions.Add(new CollisionInfo(owner, other, difference.Normalized(), difference));
        //        }
        //    }
        //    return collisions;
        //}

        internal override CollisionInfo SetCollisionTimeOfImpact(CollisionInfo collision)
        {
            if (collision.other.collider is CircleCollider)
            {
                Entity other = collision.other;

                // ax^2 + bc + c = 0, What we are trying to solve, the quadratic equation. We do that using the abc formula.
                float a = Mathf.Pow(owner.Velocity.Length, 2);
                if (Vec2.Approximately(a, 0))
                    return null;

                Vec2 relativePosition = colliderPosition - other.Position;
                float b = 2 * relativePosition.Dot(owner.Velocity);
                float c = Mathf.Pow(relativePosition.Length, 2) - Mathf.Pow(other.radius + other.radius, 2);

                if (c < 0)
                    if (b < 0)
                    {
                        collision.SetNormal(relativePosition.Normalized());
                        return collision.SetTOI(0);
                    }
                    else
                        return null;

                float d = Mathf.Pow(b, 2) - (4 * a * c); // Discriminant
                if (d < 0)
                    return null;

                //Console.WriteLine("a: {0}, \nb: {1}, \nc: {2}, \nd: {3} \n ", a, b, c, d);

                float t = (-b - Mathf.Sqrt(d)) / (2 * a);
                float t2 = (-b + Mathf.Sqrt(d)) / (2 * a);

                DrawPOICircle(CalculatePOI(t), other.radius);
                DrawPOICircle(CalculatePOI(t2), other.radius);

                if (0 <= t && t < 1)
                {
                    collision.SetNormal((CalculatePOI(t) - other.Position).Normalized());
                    return collision.SetTOI(t);                    
                }
                return null;
            }
            return null;
        }
    } 
}
