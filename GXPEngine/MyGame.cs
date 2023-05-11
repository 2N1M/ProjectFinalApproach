using System;
using GXPEngine;
using GXPEngine.Core;
using System.Drawing;

public class MyGame : Game 
{
    public static Canvas _circleCointainer = null;

    public MyGame() : base(800, 600, false, pPixelArt: true)
	{
        _circleCointainer = new Canvas(width, height);
        AddChild(_circleCointainer);

        targetFps = 60;

        //Texture2D texture = new Texture2D("circle.png");
		Entity entity1 = EntityManager.Instance.CreateEntity(new Vec2(width/2-100, height/2), 30);
		entity1.Velocity = new Vec2(1, 0);
		AddChild(entity1);

        Entity entity2 = EntityManager.Instance.CreateEntity(new Vec2(width / 2 + 100, height / 2), 40);
        entity2.Velocity = new Vec2(-1, 0);
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