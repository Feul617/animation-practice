using Unity.Entities;
using UnityEngine;

namespace Script
{
    public class AnimationSyncMono : MonoBehaviour
    {
        public GameObject animatorObject;
    }

    public class AnimationSyncBake : Baker<AnimationSyncMono>
    {
        public override void Bake(AnimationSyncMono authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<AnimationSyncTag>(entity);
            //class는 addobject로
            AddComponentObject(entity, new AnimationSyncObject(){GameObject = authoring.animatorObject});
        }
    }
}