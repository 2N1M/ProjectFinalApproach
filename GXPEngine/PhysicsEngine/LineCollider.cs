using GXPEngine.Core;
using GXPEngine.PhysicsEngine;
using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.PhysicsEngine
{
    public class LineCollider : Collider
    {
        public LineCollider(Entity owner) : base(owner)
        {
        }

        //List<CollisionInfo> CheckLineSegmentCollisions()
        //{
        //    List<CollisionInfo> collisions = new List<CollisionInfo>();

        //    MyGame myGame = (MyGame)game;
        //    CollisionInfo collision;

        //    for (int i = 0; i < myGame.GetNumberOfLines(); i++)
        //    {
        //        LineSegment lineSegment = myGame.GetLine(i);

        //        // Scalar projection of difference vector between start line segment vector to circle _position, onto line segment normal.
        //        // This gives distance between line and circle position
        //        Vec2 difference = _oldPosition - lineSegment.start;
        //        Vec2 normal = lineSegment.Normal;

        //        collision = new CollisionInfo(normal, difference, lineSegment);

        //        CollisionInfo updatedCollision = SetTimeOfImpact(collision);
        //        if (updatedCollision != null)
        //            collisions.Add(updatedCollision);
        //    }
        //    return collisions;
        //}
    }
}
