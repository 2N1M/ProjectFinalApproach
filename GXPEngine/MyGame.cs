using System;
using GXPEngine;
using GXPEngine.Core;
using System.Drawing;

public class MyGame : Game 
{

	public MyGame() : base(800, 600, false)
	{
		Entity entity1 = EntityManager.Instance.CreateEntity(new Vec2(width/2-100, height/2), 50, Color.OrangeRed);
		entity1.Velocity = new Vec2(3, 0);
		AddChild(entity1);

        Entity entity2 = EntityManager.Instance.CreateEntity(new Vec2(width / 2 + 100, height / 2), 30, Color.Orchid);
        entity2.Velocity = new Vec2(-3, 0);
		AddChild(entity2);
	}

	void Update() 
	{
		EntityManager.Instance.Step();
	}

	static void Main()
	{
		new MyGame().Start();
	}
}