using System;
namespace GXPEngine
{
    public class Missile : Sprite
    {
        private Level level;
        Sound _missileSound;

        public Missile(Level level) : base("missile.png")
        {
            _missileSound = new Sound("explosion.mp3", false, false);
            this.level = level;
        }

        

        void Update()
        {
            if (!level.gameIsPaused) { 
            y = y - 2;
            }
        }

        void OnCollision (GameObject other)
        {
            // Dont do OnCollision twice
            if (other is Ufo)
            {
                _missileSound.Play();
                other.LateDestroy();
                LateDestroy();
            }
        }
    }
}
