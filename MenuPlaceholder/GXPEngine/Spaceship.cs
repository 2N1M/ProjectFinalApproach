using System;
namespace GXPEngine
{

    public class Spaceship : Sprite
    {
        private Level level;

        float speedX;
        float speedY;

        public Spaceship(Level level) : base("spaceship.png")
        {
            this.level = level;
            this.x = 350;
            this.y = 700;
            this.scaleX = 0.3f;
            this.scaleY = 0.3f;

        }



        void Update()
        {
            if (!level.gameIsPaused) { 
            if (Input.GetKey(Key.LEFT))
            {
                speedX = speedX - 1.0f;
            }
            if (Input.GetKey(Key.RIGHT))
            {
                speedX = speedX + 1.0f;
            }
            if (Input.GetKey(Key.UP))
            {
                speedY = speedY - 1.0f;
            }
            if (Input.GetKey(Key.DOWN))
            {
                speedY = speedY + 1.0f;
            }

            x = x + speedX;
            y = y + speedY;

            speedX = speedX * 0.9f;
            speedY = speedY * 0.9f;

            if (this.x > 680)
            {
                x = 680; 
            }

            if (this.x < 20)
            {
                x = 20;
            }
            if (this.y > 700)
            {
                y = 700;
            }
            if (this.y < 500)
            {
                y = 500;
            }

            if (Input.GetKeyDown(Key.SPACE))
            {
                Missile missile = new Missile(level);
                missile.SetXY(x + width/3 , y);
                parent.AddChild(missile);
                
            }
            }



        }
    }
}