using Leopotam.Ecs;
using UnityEngine;

namespace Player
{
    public class PlayerInputSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerInputData> filter; 
        public void Run ()
        {
            foreach (var entity in filter)
            {
                ref var input = ref filter.Get1(entity);
                input.moveInput = new Vector3(SimpleInput.GetAxis("Horizontal"), 0f, SimpleInput.GetAxis("Vertical")); 
            }
        }
    }
}