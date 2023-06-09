using System;
using GXPEngine;
using GXPEngine.Core;
using GXPEngine.PhysicsEngine;
using System.Drawing;

public class MyGame : Game 
{
    public static Canvas _lineContainer = null;
    public static Canvas _circleCointainer = null;
    public bool HasStarted;
    Menu menu;
    public MyGame() : base(1920, 1080, false, pPixelArt: true)
	{
        _lineContainer = new Canvas(width, height);
        AddChild(_lineContainer);

        _circleCointainer = new Canvas(width, height);
        AddChild(_circleCointainer);

        targetFps = 60;

       // LoadExampleLevel1();
        //

        menu = new Menu();
        AddChild(menu);
            
      
    }

  public  void LoadLevel( )
    {
         if (menu.StartPressed1 == true)
        {
            LoadExampleLevel1();
            HasStarted = true;
        }

        if (menu.StartPressed2 == true)
        {
            LoadExampleLevel2();
            HasStarted = true;
        }
    }
    void LoadExampleLevel1()
    {
        Vec2 position = new Vec2(width / 2 - 260, height / 2 + 100);
        Bubble bubble1 = new Bubble(position, 100f, 200, 600, ColorAB.A, true);
        AddChild(bubble1);

        position = new Vec2(width / 2 + 460, height / 2 - 100);
        Bubble bubble3 = new Bubble(position, 100f, 200, 600, ColorAB.A, true);
        AddChild(bubble3);

        position = new Vec2(width / 2 + 450, height / 2 + 250);
        Bubble bubble2 = new Bubble(position, 10f, 100, 400, ColorAB.A, velocity: new Vec2(-1, 3));
        AddChild(bubble2);

        Entity sat = EntityManager.Instance.CreateEntity(new Vec2((width / 2) - 500, height / 2 - 150), 40, ColorAB.B);
        sat.Velocity = new Vec2(-2f, 2);
        sat.Density = 0.01f;
        AddChild(sat);

        position = new Vec2(width / 2 + 500, height / 2 - 400);
        Texture2D texture = new Texture2D("Players/NewPlayer1.png");
        Texture2D idletexture = new Texture2D("Players/NewPlayer1Idle.png");
        Player player = new Player(idletexture, 6, 5);
        player.SetCollider(ColliderType.Circle);
        player.Position = position;
        player.Density = 0.1f;
        AddChild(player);
    }

  public  void LoadExampleLevel2()
    {
        //Entity hole = EntityManager.Instance.CreateEntity(new Vec2((width / 2), height / 2), 300, ColorAB.A, ColliderType.InsideCircle);
        //hole.IsStatic = true;
        //AddChild(hole);

        Vec2 position = new Vec2(width / 2, height / 2);
        Bubble bubble1 = new Bubble(position, 20f, 300, 600, ColorAB.A, true);
        AddChild(bubble1);

        Entity sat = EntityManager.Instance.CreateEntity(new Vec2((width / 2), height / 2 - 100), 40, ColorAB.B);
        sat.Velocity = new Vec2(3f, 6f);
        sat.Density = 0.01f;
        AddChild(sat);

        position = new Vec2(width / 2+10, height / 2);
        Texture2D texture = new Texture2D("Players/NewPlayer1.png");
        Texture2D idletexture = new Texture2D("Players/NewPlayer1Idle.png");
        Player player = new Player(idletexture, 6, 5);
        player.SetCollider(ColliderType.Circle);
        player.Position = position;
        player.Density = 0.1f;
        AddChild(player);

  

        HUD hud = new HUD();
        AddChild(hud);


    }

    public void DrawLine(Vec2 start, Vec2 end)
    {
        _lineContainer.graphics.DrawLine(Pens.White, start.x, start.y, end.x, end.y);
    }

    void Update() 
	{
		EntityManager.Instance.Step();
        if (!HasStarted)
        {
  LoadLevel();
        }
      
    }

	static void Main()
	{
		new MyGame().Start();
	}
}