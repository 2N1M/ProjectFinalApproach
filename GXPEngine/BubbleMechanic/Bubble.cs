using System;
using System.Drawing;
using GXPEngine;
using GXPEngine.Core;
using GXPEngine.PhysicsEngine;

public class Bubble : GameObject
{
    public Entity BubbleEntity { get; private set; }
	public Bubble(Vec2 position, float density, int colliderRadius, int gravityRadius, ColorAB color, bool isStatic = false, Vec2? velocity = null)
	{
        BubbleEntity = EntityManager.Instance.CreateEntity(position, colliderRadius, color);
        BubbleEntity.Density = density;
        BubbleEntity.Velocity= velocity?? Vec2.Zero;
        BubbleEntity.IsStatic= isStatic;

        Entity gravity = EntityManager.Instance.CreateEntity(position, gravityRadius, color, ColliderType.GravityArea);
        gravity.ShapeAlpha = 0.1f;
        EntityManager.Instance.AddChild(BubbleEntity, gravity);
        AddChild(BubbleEntity);
        AddChildAt(gravity, 0);
    }
}
