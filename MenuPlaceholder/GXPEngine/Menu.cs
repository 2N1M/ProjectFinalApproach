using System;
namespace GXPEngine
{
    public class Menu : GameObject
    {
        StartButton _start;
        Controls _controls;
        Exit _exit;
        bool _hasStarted;
        Level level;

        public Menu() : base()
        {
            _hasStarted = false; 

            _start = new StartButton();
            AddChild(_start);
            _start.x = (game.width - _start.width) / 2;
            _start.y = ((game.height - _start.height) / 2 )- 50;

            _controls = new Controls();
            AddChild(_controls);
            _controls.x = (game.width - _controls.width) / 2;
            _controls.y = ((game.height - _controls.height) / 2);

            _exit = new Exit();
            AddChild(_exit);
            _exit.x = (game.width - _exit.width) / 2;
            _exit.y = ((game.height - _exit.height) / 2)+50;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (_start.HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    startGame();
                    hideMenu();
                    
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (_controls.HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (_exit.HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    Environment.Exit(0);

                }
            }
        }

        void hideMenu()
        {
            _start.visible = false;
            _controls.visible = false;
            _exit.visible = false;
        }
        void startGame()
        {
            if (_hasStarted == false)
            {
                 level = new Level();
                AddChild(level);
                _hasStarted = true;

                
            }
        }
    }
}
