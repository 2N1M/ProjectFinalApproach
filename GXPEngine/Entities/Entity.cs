using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using GXPEngine;
using GXPEngine.Core;
using GXPEngine.PhysicsEngine;


public class Entity : RigidBody
{
    EntityData entityData;
    public Color shapeColor;
    EasyDraw entityShape;

    public Entity(Texture2D spriteSheet = null, int cols = 1, int rows = 1, int frames = -1, int radius = 0, Color? color = null, bool easyDraw = false) : base(spriteSheet, cols, rows, frames)
    {
        entityData = new EntityData();

        this.easyDraw = easyDraw;
        this.radius = radius;

        if (easyDraw)
        {
            shapeColor = color ?? Color.FromArgb(Utils.Random(0, 255), Utils.Random(0, 255), Utils.Random(0, 255));
            entityShape = new EasyDraw(radius * 2 + 1, radius * 2 + 1);
            entityShape.SetOriginCenter();
            AddChild(entityShape);
        }        
    }

    internal override void DrawShape()
    {
        //entityShape.ClearTransparent();
        entityShape.ShapeAlign(CenterMode.Center, CenterMode.Center);
        if (collider is CircleCollider)
        {
            entityShape.Fill(shapeColor);
            entityShape.NoStroke();
            entityShape.Ellipse(width / 2, height / 2, 2 * radius, 2 * radius);
        }
        else if (collider is GXPEngine.PhysicsEngine.BoxCollider)
        {
            entityShape.Fill(shapeColor);
            entityShape.NoStroke();
            entityShape.Rect(0, 0, width, height);
        }
    }

    public void SetCollider(ColliderType colliderType)
    {
        switch (colliderType)
        {
            case ColliderType.Box:
                collider = new GXPEngine.PhysicsEngine.BoxCollider(this);
                break;
            case ColliderType.Line:
                collider = new LineCollider(this);
                break;
            case ColliderType.Circle:
                collider = new CircleCollider(this);
                break;
            case ColliderType.Trigger:
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
