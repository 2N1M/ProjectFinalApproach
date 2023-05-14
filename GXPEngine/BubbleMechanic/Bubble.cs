using System;
using System.Drawing;
using GXPEngine;
using GXPEngine.Core;
using GXPEngine.PhysicsEngine;

public class Bubble : GameObject
{
    public Entity Entity { get; private set; }
	public Bubble(Vec2 position, float density, int colliderRadius, int gravityRadius, Color color, Vec2? velocity = null)
	{
        Entity = EntityManager.Instance.CreateEntity(position, colliderRadius, color);
        Entity.Density = density;
        Entity.Velocity= velocity?? Vec2.Zero;
        Entity gravity = EntityManager.Instance.CreateEntity(position, gravityRadius, color, ColliderType.GravityArea);
        gravity.ShapeAlpha = 0.1f;
        EntityManager.Instance.AddChild(Entity, gravity);
        AddChild(Entity);
        AddChildAt(gravity, 0);
    }
}
