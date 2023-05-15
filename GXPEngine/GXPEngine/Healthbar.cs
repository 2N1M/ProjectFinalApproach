using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class Healthbar : AnimationSprite
{
 public int treshold;

    public Healthbar() : base("HealthBarRightB.png",6,6)
        {
        SetXY(game.width-this.width*6, -this.height );
        this.scale = 6.0f;
        treshold = 0; // 35 max animations
        }

    void Update()
    {

        if (treshold <= 34)
        {
            if (Input.GetMouseButton(0))
            {


                treshold += 1;

            }

            SetFrame(treshold);
        }
        else SetFrame(34);
    
    }
}

