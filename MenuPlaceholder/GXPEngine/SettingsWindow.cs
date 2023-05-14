using System;
using GXPEngine;

public class SettingsWindow : GameObject
{

    Mute _mute;
    Back _back;
    PauseMenu _pauseMenu;
    Level _level;

    bool buttonsActive = true;

    public SettingsWindow(Level level ) : base()

    {
        _level = level;
        _mute = new Mute();
        AddChild(_mute);
        _mute.x = (game.width - _mute.width) / 2;
        _mute.y = ((game.height - _mute.height) / 2)-100;

        _back = new Back();
        AddChild(_back);
        _back.x = (game.width - _back.width) / 2;
        _back.y = ((game.height - _back.height) / 2) - 50;

    }




    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_mute.HitTestPoint(Input.mouseX, Input.mouseY))
            {
                //to do: mute game

                //SoundChannel.MasterVolume = 0.0f;
                buttonsActive = false;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (_back.HitTestPoint(Input.mouseX, Input.mouseY))
            {
                _pauseMenu = new PauseMenu(_level);
                AddChild(_pauseMenu);

                hideMenu();
                buttonsActive = false;

            }
        }
    }

    void hideMenu()
    {
        _mute.visible = false;
        _back.visible = false;
    }
    public void showPause()
    {

        _mute.visible = true;
        _back.visible = true;
        buttonsActive = true;
    }

}
