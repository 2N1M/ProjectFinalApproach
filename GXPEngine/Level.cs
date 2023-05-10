using System;
using System.Collections.Generic;
using GXPEngine;

public class Level : GameObject
{
	EasyDraw background;

	public Level(int width, int height)
	{
		background = new EasyDraw(width, height);
	}
}