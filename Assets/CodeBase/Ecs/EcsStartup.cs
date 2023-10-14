using Leopotam.Ecs;
using Factories;
using Player;
using UnityEngine;
using Stacks;

public class EcsStartup : MonoBehaviour
{
    //data
    [SerializeField] private StaticData _configuration;
    [SerializeField] private SceneData _sceneData;
    [SerializeField] private RuntimeData _runtimeData;

    //ecs
    private EcsWorld _ecsWorld;

    //systems
    private EcsSystems _updateSystems;
    private EcsSystems _fixedUpdateSystems;


    private void Start()
    {
        _runtimeData = new();
        _ecsWorld = new EcsWorld();

        _updateSystems = new EcsSystems(_ecsWorld); 
        _fixedUpdateSystems = new EcsSystems(_ecsWorld);

        _updateSystems
            .Add(new LevelInitSystem())
            .Add(new PlayerInitSystem())
            .Add(new PlayerInputSystem())
            .Add(new PlayerRotationSystem())
            .Add(new StackPusherSystem())
            .Add(new StackDisposerSystem())
            .Add(new TimerSystem())
            .Add(new FactorySystem())
            .Inject(_configuration)
            .Inject(_sceneData)
            .Inject(_runtimeData);

        _fixedUpdateSystems
            .Add(new PlayerMoveSystem())
            .Inject(_configuration)
            .Inject(_sceneData)
            .Inject(_runtimeData);

        _updateSystems.Init();
        _fixedUpdateSystems.Init();
    }

    private void Update() => 
        _updateSystems.Run();
    private void FixedUpdate() => 
        _fixedUpdateSystems.Run();

    private void OnDestroy()
    {
        _updateSystems?.Destroy(); 
        _updateSystems = null;
        _fixedUpdateSystems?.Destroy();
        _fixedUpdateSystems = null;
        _ecsWorld?.Destroy();
        _ecsWorld = null;
    }
}
