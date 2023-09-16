using System;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Script
{
    public partial struct AnimationSyncSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged);
            //단일로 query를 쓰지 않으려면 class가 Managed이기 때문에 SystemAPI.ManagedAPI.GetComponent<>()
            foreach (var (syncObject, entity) in SystemAPI.Query<AnimationSyncObject>().WithEntityAccess())
            {
                var go = Object.Instantiate(syncObject.GameObject);
                
                go.AddComponent<EntityObjectMono>().AssignEntity(entity, state.EntityManager);
                
                ecb.AddComponent(entity, new EntityTransform(){Transform = go.transform});
                ecb.AddComponent(entity, new AnimationSyncAuthoring(){Animation = go.GetComponent<Animator>()});
                //지울때는 object와 상관없이 ecb로
                ecb.RemoveComponent<AnimationSyncObject>(entity);
            }

            foreach (var (goTransform, goAnimator, transform) in SystemAPI.Query<EntityTransform, AnimationSyncAuthoring, LocalTransform>())
            {
                goTransform.Transform.position = transform.Position;
                goTransform.Transform.rotation = transform.Rotation;
            }

            foreach (var (goTransform, entity) in SystemAPI.Query<EntityTransform>().WithNone<LocalToWorld>().WithEntityAccess())
            {
                GameObject.Destroy(goTransform.Transform.gameObject);
                //ecb.RemoveComponent(entity, new CleanUpTransformGO(), entity);
                ecb.RemoveComponent<EntityTransform>(entity);
            }
        }

    }
}