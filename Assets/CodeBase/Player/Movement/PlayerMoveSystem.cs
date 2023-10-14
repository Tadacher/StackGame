using Leopotam.Ecs;
using UnityEngine;

namespace Player
{
    public class PlayerMoveSystem : IEcsRunSystem
    {
        private EcsFilter<Player, PlayerInputData> _filter;

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var player = ref _filter.Get1(entity);
                ref var input = ref _filter.Get2(entity);

                
                Vector3 direction = (Vector3.forward * input.moveInput.z + Vector3.right * input.moveInput.x).normalized;
                player.PlayerRigidbody.AddForce(direction * player.PlayerSpeed);
            }
        }
    }
}