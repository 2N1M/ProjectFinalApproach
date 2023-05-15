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
        BubbleEntity = EntityManager.Instance.CreateEntity(position, colliderRadius, color, ColliderType.InsideCircle);
        BubbleEntity.Density = density;
        BubbleEntity.Velocity= velocity?? Vec2.Zero;
        BubbleEntity.IsStatic= isStatic;

        //Entity inwardsGravity = EntityManager.Instance.CreateEntity(position, gravityRadius, color, ColliderType.GravityArea);
        //inwardsGravity.ShapeAlpha = 0.1f;
        //EntityManager.Instance.AddChild(BubbleEntity, inwardsGravity);
        //AddChild(BubbleEntity);
        //AddChildAt(inwardsGravity, 0);

        Entity outwardsGravity = EntityManager.Instance.CreateEntity(position, colliderRadius - 5, color, ColliderType.GravityArea, GravityDirection.Outwards);
        outwardsGravity.ShapeAlpha = 0.1f;
        EntityManager.Instance.AddChild(BubbleEntity, outwardsGravity);
        AddChild(BubbleEntity);
        AddChildAt(outwardsGravity, 0);
    }
}
