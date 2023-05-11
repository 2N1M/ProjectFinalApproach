using System;
using System.Collections.Generic;
using System.Linq;
using GXPEngine;
using GXPEngine.Core;
using GXPEngine.PhysicsEngine;

public class Entity : RigidBody
{
    EntityData entityData;

    public Entity(Texture2D spriteSheet = null, int cols = 1, int rows = 1, int frames = -1) : base(spriteSheet, cols, rows, frames)
    {
        entityData = new EntityData();
    }

    // Circle easydraw
    //

    public void DamageEntity(int damage)
    {
        if (this.GetType() != typeof(Object))
        {
            entityData.Health -= damage;
        }
    }
}
