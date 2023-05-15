
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class Oxygenbar : AnimationSprite
{
    public int currentOxygenAmount;
    private Timer oxygenTimer;

    public Oxygenbar() : base("OxygenBarRightB.png", 6, 6)
    {
        SetXY(game.width+20 -this.width*6, this.height - 50 );
        this.scale = 6.0f;
        currentOxygenAmount = 0;

        oxygenTimer = new Timer(DecreaseOxygen, null, 0.001f);
        oxygenTimer.Start();
    }

    void Update()
    {
        Console.WriteLine(currentOxygenAmount);
        SetFrame(currentOxygenAmount);

        if(currentOxygenAmount >= 35)
        {
            oxygenTimer.LateDestroy();
        }
       
    }


    private void DecreaseOxygen(object state)
    {
        if (currentOxygenAmount < 33)
        {
            currentOxygenAmount += 1;
            oxygenTimer = new Timer(DecreaseOxygen, null, 1.0f);
            oxygenTimer.Start();
        }
        
     
        else SetFrame(33);   
    }
}
