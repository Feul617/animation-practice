using System;
using Unity.Entities;
using UnityEngine;

namespace Script
{
    public class EntityObjectMono : MonoBehaviour
    {
        public Entity Entity;
        public EntityManager EntityManager;

        public void AssignEntity(Entity e, EntityManager em)
        {
            Entity = e;
            EntityManager = em;
        }

        public void OnDestroy()
        {
            if (EntityManager != null)
            {
                EntityManager.DestroyEntity(Entity);
            }
        }
    }
}