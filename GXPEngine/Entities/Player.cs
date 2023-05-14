using System;
using GXPEngine;
using GXPEngine.Core;

public class Player : Entity
{
    Easing rotateEase = new Easing();
    public float spriteRotation = 90;
    float playerSpeed = 0.06f;

    public Player(Texture2D spriteSheet = null, int cols = 1, int rows = 1, int frames = -1) : base(spriteSheet, cols, rows , frames, 64)
    {
        bounciness = 0;
        SetCycle(0, 5);
        scale = 1.9f;
    }

    void PlayerControls()
    {
        if (Input.GetKey(Key.W))
        {
            movementAccelaration.y = -playerSpeed;
        }
        else if (Input.GetKey(Key.S))
        {
            movementAccelaration.y = playerSpeed;
        }
        else
        {
            movementAccelaration.y = 0;
        }

        if (Input.GetKey(Key.A))
        {
            movementAccelaration.x = -playerSpeed;            
        }
        else if (Input.GetKey(Key.D))
        {
            movementAccelaration.x = playerSpeed;            
        }
        else
        {
            movementAccelaration.x = 0;
        }

        if (Input.GetKeyDown(Key.SPACE))
        {
            hitAccelaration = gravityAcceleration.Normalized().Invert() * 3f;
        }
    }

    void RotatePlayer()
    {
        rotation = rotateEase.EaseAngle(rotation, gravityAcceleration.GetAngleDegrees() - spriteRotation, 0.09f);

        FlipPlayer(); // TODO: Find better way to do this, malfunctions when walking into other
    }

    void FlipPlayer()
    {
        if (Velocity.Length > 0.4f && Mathf.Abs(Velocity.x) > 0.3f)
        {
            if (Velocity.x < 0 && scaleX > 0)
            {
                scaleX *= -1; // left side
            }
            else if (Velocity.x > 0 && scaleX < 0)
            {
                scaleX *= -1; // right side
            }

            float currentRotation = rotation + spriteRotation;
            if (currentRotation > 0 && currentRotation < 180)
            {

                if (Velocity.x < 0 && scaleX > 0)
                {
                    scaleX *= -1; // left side
                }
                else if (Velocity.x > 0 && scaleX < 0)
                {
                    scaleX *= -1; // right side
                }
            }
            else // Top half
            {
                if (Velocity.x > 0 && scaleX > 0)
                {
                    scaleX *= -1; // left side
                }
                else if (Velocity.x < 0 && scaleX < 0)
                {
                    scaleX *= -1;
                }
            }
        }
    }

    void Update()
    {
        //NewPlayer1 cycle
        //if (inverted)
        //{
        //    SetCycle(6, 11);
        //}
        //else
        //{
        //    SetCycle(0, 5);
        //}

        //NewPlayer1Idle cycle
        if (EntityManager.Instance.inverted)
        {
            SetCycle(0, 12);
        }
        else
        {
            SetCycle(13, 12);            
        }

        PlayerControls();
        RotatePlayer();
        
        Animate(0.5f);
    }
}
