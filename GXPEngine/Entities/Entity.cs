﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using GXPEngine;
using GXPEngine.Core;
using GXPEngine.PhysicsEngine;

public enum ColorAB
{
    A,
    B
}

public class Entity : RigidBody
{
    EntityData entityData;
    public Color shapeColor;
    EasyDraw entityShape;

    ColorAB entityColor = ColorAB.A;

    public float ShapeAlpha
    {
        get { return entityShape.alpha; }
        set { entityShape.alpha = value; }
    }

    public Entity(Texture2D spriteSheet = null, int cols = 1, int rows = 1, int frames = -1, int radius = 0, ColorAB? color = null, bool easyDraw = false) : base(spriteSheet, cols, rows, frames)
    {
        entityData = new EntityData();

        this.easyDraw = easyDraw;
        this.radius = radius;

        if (easyDraw)
        {
            entityColor = color ?? ColorAB.A;
            entityShape = new EasyDraw(radius * 2 + 1, radius * 2 + 1);
            entityShape.SetOriginCenter();
            AddChild(entityShape);
        }

        EntityManager.Instance.GetEntities().Add(this);
    }

    internal override void DrawShape()
    {
        switch (entityColor)
        {
            case ColorAB.A:
                shapeColor = EntityManager.Instance.entityAColor;
                break;
            case ColorAB.B:
                shapeColor = EntityManager.Instance.entityBColor;
                break;
        }

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

    public void SetCollider(ColliderType colliderType, GravityDirection gravityDirection = GravityDirection.Inwards)
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
            case ColliderType.InsideCircle:
                collider = new InsideCircleCollider(this);
                break;
            case ColliderType.GravityArea:
                collider = new GravityArea(this, gravityDirection);
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
