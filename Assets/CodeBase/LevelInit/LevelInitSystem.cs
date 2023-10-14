using Factories;
using Leopotam.Ecs;
using Stacks;

/// <summary>
/// Bootstrap level and create basic entities
/// </summary>
public class LevelInitSystem : IEcsInitSystem
{
    private EcsWorld _ecsWorld;
    private SceneData _sceneData;
    public void Init()
    {
        ConstructFactories();
        ConstructRecievers();
    }

    private void ConstructRecievers()
    {
        foreach (var recieverEntity in _sceneData.ItemRecievers)
        {
            EcsEntity ecsEntity =  _ecsWorld.NewEntity();
            ConstructStackComponent(recieverEntity.StackComponent, ecsEntity);
            ConstructStackDisposerComponent(ecsEntity);
        }
    }

   

    private void ConstructFactories() // тут тоже можно было-бы нагородить абстрактных фабрик или типа того, чтобы собирать сущности вне зависимости от декларированных компонентов
                                      // но когда мне пришла эта идея, меня уже начинало поджимать время и получилось сумбурно (и не только тут =D)
    {
        foreach (var factoryEntity in _sceneData.Factories)
        {
            EcsEntity ecsEntity = _ecsWorld.NewEntity();

            ConstructFactory(factoryEntity.Factory, ecsEntity);
            ConstructStackComponent(factoryEntity.StackComponent, ecsEntity);
            ConstructStackPusher(factoryEntity.StackPusherData, ecsEntity);
            ConstructTimer(factoryEntity.Timer, ecsEntity);
        }
    }

    private static void ConstructFactory(Factory factoryPrototype, EcsEntity ecsEntity)
    {
        ref var factory = ref ecsEntity.Get<Factory>();
        factory.ProductPrefab = factoryPrototype.ProductPrefab;
    }

    private static void ConstructTimer(Timer timerPrototype, EcsEntity ecsEntity)
    {
        ref var timer = ref ecsEntity.Get<Timer>();
        timer.ProduceTime = timerPrototype.ProduceTime;
        timer.TimeLeft = timer.ProduceTime;
        ecsEntity.Get<TimerEndFlag>();
    }

    private static void ConstructStackPusher(StackPusher stackPusherPrototype, EcsEntity ecsEntity)
    {
        ref var stackPusher = ref ecsEntity.Get<StackPusher>();
        stackPusher.PusherTransform = stackPusherPrototype.PusherTransform;
        stackPusher.TargetLayer = stackPusherPrototype.TargetLayer;
        stackPusher.Radius = stackPusherPrototype.Radius;
    }

    private void ConstructStackComponent(StackComponent component, EcsEntity ecsEntity)
    {
        ref var stack = ref ecsEntity.Get<StackComponent>();
        stack.Stack = new();
        stack.StackInterval = component.StackInterval;
        stack.StackTransform = component.StackTransform;
        StackPusherTargetView view = component.StackTransform.GetComponent<StackPusherTargetView>();
        stack.View = view;
        view.Entity = ecsEntity;
    }
    private void ConstructStackDisposerComponent(EcsEntity ecsEntity) =>
       ecsEntity.Get<StackDisposerComponent>();
}