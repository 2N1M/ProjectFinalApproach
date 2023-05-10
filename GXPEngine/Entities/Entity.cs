using System;
using GXPEngine;

public class Entity
{
    public bool isObject;
    EntityData entityData;

    public Entity(bool isObject = false)
    {
        entityData = new EntityData();
        this.isObject = isObject;
    }

    public void DamageEntity(int damage)
    {
        if (!isObject)
        {
            entityData.Health -= damage;
        }
    }
}
