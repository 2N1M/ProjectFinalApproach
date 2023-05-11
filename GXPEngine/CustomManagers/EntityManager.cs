using GXPEngine.PhysicsEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            { // move everything step-by-step: in one frame, only one mover moves
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
            { // move all movers every frame
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
