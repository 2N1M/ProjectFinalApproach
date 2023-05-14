using System;					
using GXPEngine;                            
using System.Drawing;			

public class MyGame : Game
{

	public MyGame() : base(800, 800, false)	
	{
		Menu menu = new Menu();
		AddChild(menu);

	}

	void Update()
	{
		
	}

	static void Main()						
	{
		new MyGame().Start();				
	}
}