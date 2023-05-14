using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;

public class PauseMenu : GameObject
{
    Play _play;
    SettingsButton _settings;
    Exit _exit;
    SettingsWindow _settingsWindow;
    Level _level;
    bool buttonsActive = true;

    public PauseMenu(Level level) : base()
    {
        _level = level;

        _play = new Play();
        AddChild(_play);
        _play.x = (game.width - _play.width) / 2;
        _play.y = ((game.height - _play.height) / 2) - 150;  

        _settings = new SettingsButton();               
        AddChild(_settings);
        _settings.x = (game.width - _settings.width) / 2;
        _settings.y = ((game.height - _settings.height) / 2) - 100;

        _exit = new Exit();                       
        AddChild(_exit);
        _exit.x = (game.width - _exit.width) / 2;
        _exit.y = ((game.height - _exit.height) / 2) -50;
    }


    void Update()
    {
       
        if (Input.GetMouseButtonDown(0))
        {
            if (_play.HitTestPoint(Input.mouseX, Input.mouseY) && buttonsActive)
            {
                _level.gameIsPaused = false;
                this.LateDestroy();
            }
            
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (_settings.HitTestPoint(Input.mouseX, Input.mouseY) && buttonsActive)
            {
                _settingsWindow = new SettingsWindow(_level);
                AddChild(_settingsWindow);
                
                hidePause();
              
                buttonsActive = false;
                
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (_exit.HitTestPoint(Input.mouseX, Input.mouseY) && buttonsActive)
            {

                hidePause();

                //     _level.LateDestroy();

            }

        }


    }

    void hidePause()
    {
        _play.visible = false;
        _settings.visible = false;
        _exit.visible = false;
    }

    void reactivateButtons()
    {
        _play.visible = true;
        _settings.visible = true;
        _exit.visible = true;
        buttonsActive = true;
    }
}