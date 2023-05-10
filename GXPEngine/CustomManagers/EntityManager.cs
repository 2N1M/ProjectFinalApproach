using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EntityManager
{
    public static EntityManager Instance
    {
        get
        {
            if (instance == null)
                instance = new EntityManager();
            return instance;
        }
    }
    private static EntityManager instance;

    public List<Entity> entityList = new List<Entity>();

    public void DamageEntity(Entity entity, int damage)
    {
        if (entity == null || entity.isObject) return;
        entityList.Find(x => x == entity).DamageEntity(damage);
    }
}
