using GXPEngine.Core;
using PhysicsEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.PhysicsEngine
{
    public class CollisionInfo
    {
        public Entity self, other;
        public Vec2 normal;
        public Vec2 difference;
        public float timeOfImpact;
        public float penetrationDepth;

        public CollisionInfo(Entity pSelf, Entity pOther, Vec2 pNormal, Vec2 pPoint, float pTimeOfImpact = 0, float pPenetrationDepth = 0)
        {
            self = pSelf;
            other = pOther;
            normal = pNormal;
            difference = pPoint;
            timeOfImpact = pTimeOfImpact;
            penetrationDepth = pPenetrationDepth;
        }

        public CollisionInfo(Entity pSelf, Entity pOther, Vec2 pNormal, float pTimeOfImpact) : this(pSelf, pOther, pNormal, new Vec2(0, 0), pTimeOfImpact, 0)
        {
        }
        public CollisionInfo(Entity pSelf, Entity pOther, Vec2 pNormal, Vec2 pPoint, float pPenetrationDepth) : this(pSelf, pOther, pNormal, pPoint, 0, pPenetrationDepth)
        {
        }


        public CollisionInfo SetTOI(float toi)
        {
            timeOfImpact = toi;
            return this;
        }
        public CollisionInfo SetNormal(Vec2 normal)
        {
            this.normal = normal;
            return this;
        }

    }
}
