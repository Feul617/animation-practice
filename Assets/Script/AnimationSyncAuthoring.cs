using UnityEngine;
using Unity.Entities;

namespace Script
{
    //Animator는 class에서만 담기니까 ManagedAPI로 사용
    public class AnimationSyncAuthoring : IComponentData
    {
        public Animator Animation;
    }

    public struct AnimationSyncTag : IComponentData { }

    public class AnimationSyncObject : IComponentData
    {
        public GameObject GameObject;
    }

    public class EntityTransform : IComponentData
    {
        public Transform Transform;
    }
    
    //Entity가 파괴된 이후에 남아있는 컴포넌트 -> ICleanupComponentData
    public class CleanUpTransformGO : ICleanupComponentData
    {
        private Transform Transform;
    }
}