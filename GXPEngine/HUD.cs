using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

public class HUD : EasyDraw
{
    EasyDraw player1Elements;
    EasyDraw player2Elements;
    Healthbar _healthbar;
    Oxygenbar _oxygenbar;
    public HUD() : base(Game.main.width, Game.main.height)
    {
         _healthbar = new Healthbar();
         AddChild(_healthbar);
        _oxygenbar = new Oxygenbar();
        AddChild(_oxygenbar);

    }
    void Update()
    {

     
    }
    void DisplayHealth(EasyDraw player, int healthValue)
    {
        player.Text("Health: " + healthValue, 0, 0);
    }
}
