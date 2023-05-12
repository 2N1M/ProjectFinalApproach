using System;
using GXPEngine;
using GXPEngine.Core;
using GXPEngine.PhysicsEngine;
using System.Drawing;

public class MyGame : Game 
{
    public static Canvas _lineContainer = null;
    public static Canvas _circleCointainer = null;

    public MyGame() : base(1920, 1080, false, pPixelArt: true)
	{
        _lineContainer = new Canvas(width, height);
        AddChild(_lineContainer);

        _circleCointainer = new Canvas(width, height);
        AddChild(_circleCointainer);

        targetFps = 60;

        //Texture2D texture = new Texture2D("circle.png");
        Entity entity1 = EntityManager.Instance.CreateEntity(new Vec2(width / 2, height / 2), 100, Color.Orange);
        //entity1.Density= 1000;

        Entity gravity = EntityManager.Instance.CreateEntity(new Vec2(width/2, height/2), 400, Color.FromArgb(10,79,47,14), ColliderType.GravityArea);
        EntityManager.Instance.AddChild(entity1, gravity);

        AddChild(entity1);
        AddChildAt(gravity, 0);

        Entity entity2 = EntityManager.Instance.CreateEntity(new Vec2((width / 2)+200, height / 2+50), 40, Color.DarkSlateGray);
        entity2.Velocity = new Vec2(-1, 1);
		AddChild(entity2);
	}

    public void DrawLine(Vec2 start, Vec2 end)
    {
        _lineContainer.graphics.DrawLine(Pens.White, start.x, start.y, end.x, end.y);
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