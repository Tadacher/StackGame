using Leopotam.Ecs;
using Stacks;
using UnityEngine;

namespace Player
{

    public class PlayerInitSystem : IEcsInitSystem
    {
        private EcsWorld _ecsWorld;
        private StaticData _staticData; 
        private SceneData _sceneData;
        public void Init()
        {
            EcsEntity playerEntity = _ecsWorld.NewEntity();

            ref var player = ref playerEntity.Get<Player>();
            ref var inputData = ref playerEntity.Get<PlayerInputData>();
            ref var playerStackPusher = ref playerEntity.Get<StackPusher>();
            ref var playerStack = ref playerEntity.Get<StackComponent>();

            GameObject playerGO = GameObject.Instantiate(_staticData.PlayerPrefab, _sceneData.PlayerSpawnPoint.position, Quaternion.identity);
            StackPusherTargetView view = playerGO.GetComponent<StackPusherTargetView>();
            view.Entity = playerEntity;

            player.PlayerRigidbody = playerGO.GetComponent<Rigidbody>();
            player.PlayerSpeed = _staticData.PlayerSpeed;
            player.PlayerTransform = playerGO.transform;

            playerStackPusher.Radius = _staticData.PlayerItemDropTriggerRadius;
            playerStackPusher.PusherTransform = playerGO.transform;
            playerStackPusher.TargetLayer = "ItemReciever";

            playerStack.Stack = new();
            playerStack.StackInterval = 0.2f;
            playerStack.StackTransform = playerGO.transform;
            playerStack.View = view;

            _sceneData.VirtualCamera.Follow = player.PlayerTransform;
            _sceneData.VirtualCamera.LookAt = player.PlayerTransform;
        }
    }
}