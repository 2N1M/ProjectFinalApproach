using GXPEngine;
using GXPEngine.Core;
using GXPEngine.PhysicsEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsEngine
{
    public class CollisionManager
    {
        public static CollisionManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new CollisionManager();
                return instance;
            }
        }
        private static CollisionManager instance;

        //List<Collider> colliders = new List<Collider>();

        
    }
}
