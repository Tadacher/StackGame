using Leopotam.Ecs;

namespace Player
{
    public class PlayerRotationSystem : IEcsRunSystem
    {
        private EcsFilter<Player> _filter;
        private SceneData sceneData;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var player = ref _filter.Get1(i);

                if(player.PlayerRigidbody.velocity.sqrMagnitude>0.1)
                    player.PlayerTransform.forward = player.PlayerRigidbody.velocity;
            }
        }
    }
}