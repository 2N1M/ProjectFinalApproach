using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.PhysicsEngine
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

        List<Collider> colliderList = new List<Collider>();

        public List<Collider> GetColliders()
        {
            return colliderList;
        }

    }
}
