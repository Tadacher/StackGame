using Leopotam.Ecs;
using UnityEngine;

public class TimerSystem : IEcsRunSystem
{
    private EcsFilter<Timer, TimerEndFlag> _filter;
    public void Run()
    {
        foreach (var entity in _filter)
        {
            ref Timer timer = ref _filter.Get1(entity);
            ref TimerEndFlag reciever = ref _filter.Get2(entity);

            timer.TimeLeft -= Time.deltaTime;
            if(timer.TimeLeft < 0)
            {
                timer.TimeLeft = timer.ProduceTime;
                reciever.Value = true;
            }
        }
    }  
}
