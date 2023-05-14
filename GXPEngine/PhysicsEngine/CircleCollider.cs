using System;
using System.Collections.Generic;
using System.Drawing;
using GXPEngine;
using GXPEngine.Core;
using GXPEngine.PhysicsEngine;

namespace GXPEngine.PhysicsEngine
{
	public class CircleCollider : Collider
	{
        public float Radius
        {
            get { return owner.radius; }
        }

        public CircleCollider(Entity owner) : base(owner)
		{

		}

        // TODO: Circle box collisions (Basically circle and 4 line segments)
        public override bool HitTest(Collider other)
        {
            if (other is BoxCollider boxCollider)
            {
                Vec2[] c = owner.GetExtents();
                if (c == null) return false;
                Vec2[] d = boxCollider.owner.GetExtents();
                if (d == null) return false;
                if (!AreaOverlap(c, d)) return false;
                return AreaOverlap(d, c);
            }
            else if (other is CircleCollider circleCollider)
            {
                return CircleOverlap(owner.Position, circleCollider.owner.Position, owner.radius, circleCollider.owner.radius);
            }
            else
            {
                return false;
            }
        }

        internal override CollisionInfo CheckCollision(Entity other)
        {
            Vec2 difference = owner.oldPosition - other.Position;

            CollisionInfo collision = new CollisionInfo(owner, other, Vec2.Zero, difference); // Normal 0 because it still has to be calculated by TOI
            CollisionInfo updatedCollision = SetCollisionTimeOfImpact(collision);

            if (updatedCollision != null)
                return updatedCollision;
            return null;
        }

        internal override CollisionInfo SetCollisionTimeOfImpact(CollisionInfo collision)
        {
            if (collision.other.collider is CircleCollider)
            {
                Entity other = collision.other;

                // ax^2 + bc + c = 0, What we are trying to solve, the quadratic equation. We do that using the abc formula.
                float a = Mathf.Pow(owner.Velocity.Length, 2);
                if (Vec2.Approximately(a, 0))
                    return null;

                Vec2 relativePosition = owner.oldPosition - other.Position;
                float b = 2 * relativePosition.Dot(owner.Velocity);
                float c = Mathf.Pow(relativePosition.Length, 2) - Mathf.Pow(Radius + other.radius, 2);

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
                //float t2 = (-b + Mathf.Sqrt(d)) / (2 * a);

                //DrawPOICircle(CalculatePOI(t), Radius);
                //DrawPOICircle(owner.CalculatePOI(t2));

                if (0 <= t && t < 1)
                {
                    collision.SetNormal((CalculatePOI(t) - other.Position).Normalized());
                    return collision.SetTOI(t);                    
                }
                return null;
            }
            else if (collision.other.collider is LineCollider) // Other is line segment
            {
                Vec2 collisionNormal = collision.normal;

                float b = -(owner.Position - owner.oldPosition).Dot(collisionNormal); // New position or total movement along collision normal

                if (b <= 0) // Moving away
                    return null;

                float a = collision.difference.Dot(collisionNormal) - Radius; // Shortest distance between ball and line segment
                float t;
                if (a >= 0)
                    t = a / b; // TOI
                else if (a >= -Radius) // Going towards deeper collision
                    t = 0; // TOI
                else // Ball center already past line
                    return null;

                if (((LineSegment)collision.other).BetweenSegment(owner.oldPosition + owner.Velocity * t))
                {
                    if (t <= 1)
                        return collision.SetTOI(t);
                }
                return null;
            }
            return null;
        }

        public void DrawPOICircle(Vec2 poi, float radius)
        {
            MyGame._circleCointainer.graphics.DrawEllipse(Pens.White, poi.x - radius, poi.y - radius, 2 * radius, 2 * radius);
        }
    } 
}
