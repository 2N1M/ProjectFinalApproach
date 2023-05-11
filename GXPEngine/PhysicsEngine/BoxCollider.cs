using GXPEngine.Core;
using System;

namespace GXPEngine.PhysicsEngine
{
    public class BoxCollider : Collider
    {
        public BoxCollider()
        {
        }

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
    }
}
