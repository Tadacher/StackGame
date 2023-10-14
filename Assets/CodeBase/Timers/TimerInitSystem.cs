using Leopotam.Ecs;

public class TimerInitSystem : IEcsInitSystem
{
    private EcsFilter<Timer> filter;
    public void Init()
    {
        
       foreach (var entity in filter)
       {
            filter.GetEntity(entity).Get<TimerEndFlag>();
       }
    }
}
