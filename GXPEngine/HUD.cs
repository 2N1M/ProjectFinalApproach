using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class HUD : EasyDraw
{
    EasyDraw player1Elements;
    EasyDraw player2Elements;

    public HUD() : base(Game.main.width, Game.main.height)
    {

    }

    void DisplayHealth(EasyDraw player, int healthValue)
    {
        player.Text("Health: " + healthValue, 0, 0);
    }
}
