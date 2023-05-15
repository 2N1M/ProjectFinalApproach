using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

    class Menu : GameObject 
    {
    StartButton _start;
    Controls _controls;
    Exit _exit;
  
  public bool  StartPressed1 , StartPressed2;
    public Menu()
    {
        _start = new StartButton();
        AddChild(_start);
        _start.x = (game.width - _start.width) / 2;
        _start.y = ((game.height - _start.height) / 2) - 50;

        _controls = new Controls();
        AddChild(_controls);
        _controls.x = (game.width - _controls.width) / 2;
        _controls.y = ((game.height - _controls.height) / 2);

        _exit = new Exit();
        AddChild(_exit);
        _exit.x = (game.width - _exit.width) / 2;
        _exit.y = ((game.height - _exit.height) / 2) + 50;

     StartPressed1  = false;
        StartPressed2 = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_start.HitTestPoint(Input.mouseX, Input.mouseY))
            {
               StartPressed1 = true;
                hideMenu();

            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (_controls.HitTestPoint(Input.mouseX, Input.mouseY))
            {
                StartPressed2 = true;
                hideMenu();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (_exit.HitTestPoint(Input.mouseX, Input.mouseY))
            {
                Environment.Exit(0);

            }
        }
        void hideMenu()
        {
            _start.visible = false;
            _controls.visible = false;
            _exit.visible = false;
        }


    }
    }

