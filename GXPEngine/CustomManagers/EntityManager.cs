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
    bool _stepped = false;
    bool _paused = false;
    int _stepIndex = 0;

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

    public List<Entity> GetEntities()
    {
        return entityList;
    }

    public void DamageEntity(Entity entity, int damage)
    {
        if (entity == null || entity.GetType() != typeof(object)) return;
        entityList.Find(x => x == entity).DamageEntity(damage);
    }

    public void Step()
    {
        if (!_paused)
        {
            if (_stepped)
            { // move everything step-by-step: in one frame, only one entity moves
                _stepIndex++;

                if (_stepIndex >= entityList.Count)
                {
                    _stepIndex = 0;
                }

                if (entityList[_stepIndex].GetType() != typeof(StaticObject))
                {
                    entityList[_stepIndex].Step();
                }
            }
            else
            { // move all entities every frame
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
