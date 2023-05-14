using System;
namespace GXPEngine
{

    public class Level : GameObject
    {
        public bool gameIsPaused { get; set; } = false;
        const int WIDTH = 7;
        const int HEIGHT = 13;
        const int SIZE = 64;

        Pause _pause;
        PauseMenu _pauseMenu;

        int[,] level = new int[HEIGHT, WIDTH]
        {
        { 0, 0, 0, 0, 0, 0, 0},
        { 0, 1, 0, 1, 0, 0, 0},
        { 0, 1, 0, 1, 0, 0, 0},
        { 0, 1, 0, 1, 0, 0, 0},
        { 0, 1, 0, 1, 0, 0, 0},
        { 0, 1, 0, 1, 0, 0, 0},
        { 0, 0, 1, 0, 1, 0, 0},
        { 0, 0, 1, 0, 1, 0, 0},
        { 0, 1, 0, 1, 0, 0, 0},
        { 0, 0, 0, 0, 1, 0, 0},
        { 0, 1, 1, 1, 0, 0, 0},
        { 0, 1, 1, 0, 1, 0, 0},
        { 0, 0, 0, 0, 0, 0, 0}
        };

        public Level()
        {
            setupLevel();
            Spaceship spaceship = new Spaceship(this);
            AddChild(spaceship);

            Missile missile = new Missile(this);
            AddChild(missile);

            _pause = new Pause();
            AddChild(_pause);
            _pause.x = (game.width - _pause.width);
            _pause.y = (game.height - _pause.height);



        }

        void Update()
        {

            if (Input.GetMouseButtonDown(0))
            {

                if (_pause.HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    gameIsPaused = true;
                    _pauseMenu = new PauseMenu(this);
                    AddChild(_pauseMenu);

                }
            }
        }

        void createTile(int row, int col, int tile)
        {
            if (tile == 1)
            {
                Ufo ufo = new Ufo();
                AddChild(ufo);
                ufo.x = col * SIZE;
                ufo.y = row * SIZE;
            }
        }

        void setupLevel()
        {
            for (int row = 0; row < HEIGHT; row++)
            {
                for (int col = 0; col < WIDTH; col++)
                {
                    int tile = level[row, col];
                    createTile(col, row, tile);
                }
            }
        }
    }
}
