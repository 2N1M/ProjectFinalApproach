using GXPEngine;
using GXPEngine.Core;
using GXPEngine.PhysicsEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

enum EntityType
{
    EasyDraw,
    Texture,
}

public class EntityManager
{
    bool stepped = false;
    bool paused = false;
    int stepIndex = 0;

    public static EntityManager Instance
    {
        get
        {
            if (instance == null)
                instance = new EntityManager();
            return instance;
        }
    }
    private static EntityManager instance;

    List<Entity> entityList = new List<Entity>();

    /// <summary>
    /// Textured CircleCollider Entity
    /// </summary>
    /// <param name="texture"></param>
    /// <param name="radius"></param>
    /// <param name="cols"></param>
    /// <param name="rows"></param>
    /// <param name="frames"></param>
    /// <returns></returns>
    public Entity CreateEntity(Vec2 position, Texture2D texture, int radius = -1, int cols = 1, int rows = 1, int frames = -1)
    {
        if (radius == -1)
        {
            radius = texture.width / 2;
        }
        Entity entity = new Entity(texture, cols, rows, frames, radius);
        entity.SetCollider(ColliderType.Circle);
        entityList.Add(entity);
        entity.Position= position;

        return entity;
    }
    /// <summary>
    /// EasyDraw CircleCollider Entity
    /// </summary>
    /// <param name="pRadius"></param>
    /// <returns></returns>
    public Entity CreateEntity(Vec2 position, int pRadius, Color? pColor = null)
    {
        Texture2D empty = new Texture2D((int)pRadius * 2, (int)pRadius * 2);
        Entity entity = new Entity(empty, radius: pRadius, color: pColor, easyDraw: true);
        entity.SetCollider(ColliderType.Circle);
        entityList.Add(entity);
        entity.Position= position;

        return entity;
    }

    public void DestroyEntity(Entity entity)
    {
        entityList.Remove(entity);
        entity.Destroy();
    }

    public List<Entity> GetEntities()
    {
        return entityList;
    }

    public void DamageEntity(Entity entity, int damage)
    {
        if (entity == null || entity.GetType() != typeof(object)) return;
        entityList.Find(x => x == entity).DamageEntity(damage);
    }

    void HandleInput()
    {
        Game.main.targetFps = Input.GetKey(Key.SPACE) ? 5 : 60;
        //if (Input.GetKeyDown(Key.UP))
        //{
        //    Ball.acceleration.SetXY(0, -1);
        //}
        //if (Input.GetKeyDown(Key.DOWN))
        //{
        //    Ball.acceleration.SetXY(0, 1);
        //}
        //if (Input.GetKeyDown(Key.LEFT))
        //{
        //    Ball.acceleration.SetXY(-1, 0);
        //}
        //if (Input.GetKeyDown(Key.RIGHT))
        //{
        //    Ball.acceleration.SetXY(1, 0);
        //}
        //if (Input.GetKeyDown(Key.BACKSPACE))
        //{
        //    Ball.acceleration.SetXY(0, 0);
        //}
        if (Input.GetKeyDown(Key.S))
        {
            stepped ^= true;
        }
        //if (Input.GetKeyDown(Key.D))
        //{
        //    Ball.drawDebugLine ^= true;
        //}
        //if (Input.GetKeyDown(Key.H))
        //{
        //    Ball.drawPOI ^= true;
        //}
        if (Input.GetKeyDown(Key.P))
        {
            paused ^= true;
        }
        //if (Input.GetKeyDown(Key.B))
        //{
        //    Ball.bounciness = 1.5f - Ball.bounciness;
        //}
        //if (Input.GetKeyDown(Key.W))
        //{
        //    Ball.wordy ^= true;
        //}
        //if (Input.GetKeyDown(Key.C))
        //{
        //    _lineContainer.graphics.Clear(Color.Transparent);
        //}
        //if (Input.GetKeyDown(Key.R))
        //{
        //    LoadScene(_startSceneNumber);
        //}
        //for (int i = 0; i < 10; i++)
        //{
        //    if (Input.GetKeyDown(48 + i))
        //    {
        //        LoadScene(i);
        //    }
        //}
    }

    public void Step()
    {
        HandleInput();

        if (!paused)
        {
            if (stepped)
            { // move everything step-by-step: in one frame, only one entity moves
                stepIndex++;

                if (stepIndex >= entityList.Count)
                {
                    stepIndex = 0;
                }

                if (entityList[stepIndex].GetType() != typeof(StaticObject))
                {
                    entityList[stepIndex].Step();
                }

                MyGame._circleCointainer.graphics.Clear(Color.Transparent);
            }
            else
            { // move all entities every frame
                MyGame._circleCointainer.graphics.Clear(Color.Transparent);
                foreach (Entity entity in entityList)
                {
                    if (entity.GetType() != typeof(StaticObject))
                    {
                        entity.Step();
                    }
                }
            }
        }
    }
}
