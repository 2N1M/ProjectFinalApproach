using System;
using GXPEngine;

public class Weight
{
    void DamageEntity(Entity entity, int damage)
    {
        if (entity.GetType() != typeof(Player))
        {
            EntityManager.Instance.DamageEntity(entity, damage);
        }
    }
}
