using System;
using GXPEngine;
using GXPEngine.Core;
using GXPEngine.PhysicsEngine;
using System.Drawing;

public class MyGame : Game 
{
    public static Canvas _lineContainer = null;
    public static Canvas _circleCointainer = null;

    Camera camera;

    public MyGame() : base(1920, 1080, false, pPixelArt: true)
	{
        _lineContainer = new Canvas(width, height);
        AddChild(_lineContainer);

        _circleCointainer = new Canvas(width, height);
        AddChild(_circleCointainer);

        targetFps = 60;

        //Texture2D texture = new Texture2D("circle.png");
        Vec2 position = new Vec2(width / 2 - 150, height / 2);
        Bubble bubble1 = new Bubble(position, 100f, 150, 1000, Color.Orange);
        AddChild(bubble1);

        position = new Vec2(width / 2 + 150, height / 2);
        Bubble bubble2 = new Bubble(position, 0.1f, 100, 400, Color.Orange, new Vec2(-1,4));
        AddChild(bubble2);

        Entity sat = EntityManager.Instance.CreateEntity(new Vec2((width / 2) + 200, height / 2 + 150), 40, Color.DarkSlateGray);
        sat.Velocity = new Vec2(-1, 3.5f);
        sat.Density = 0.01f;
        AddChild(sat);
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