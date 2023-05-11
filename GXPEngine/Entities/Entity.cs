using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using GXPEngine;
using GXPEngine.Core;
using GXPEngine.PhysicsEngine;

public enum Collider
{
    Box,
    Line,
    Circle,
    Trigger
}

public class Entity : RigidBody
{
    EntityData entityData;
    public new Color shapeColor;
    EasyDraw entityShape;

    public Entity(Texture2D spriteSheet = null, int cols = 1, int rows = 1, int frames = -1) : base(spriteSheet, cols, rows, frames)
    {
        entityData = new EntityData();
    }
    
    public Entity(float radius, Color? color = null) : base(spriteSheet, cols, rows, frames)
    {
        entityData = new EntityData();       

        shapeColor = color ?? Color.FromArgb(Utils.Random(0, 255), Utils.Random(0, 255), Utils.Random(0, 255));
    }

    public void Draw()
    {
        entityShape.Fill(shapeColor);
        entityShape.NoStroke();
        entityShape.Ellipse(width / 2, height / 2, 2 * radius, 2 * radius);
    }

    public void SetCollider(Collider colliderType)
    {
        switch (colliderType)
        {
            case Collider.Box:
                collider = new GXPEngine.PhysicsEngine.BoxCollider(this);
                break;
            case Collider.Line:
                collider = new LineCollider(this);
                break;
            case Collider.Circle:
                collider = new CircleCollider(this);
                break;
            case Collider.Trigger:
                break;
            default:
                break;
        }
    }

    public void DamageEntity(int damage)
    {
        if (this.GetType() != typeof(Object))
        {
            entityData.Health -= damage;
        }
    }
}
