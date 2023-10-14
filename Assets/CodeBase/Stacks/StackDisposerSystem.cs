using Leopotam.Ecs;
using UnityEngine;

namespace Stacks
{
    public class StackDisposerSystem : IEcsRunSystem
    {
        private EcsFilter<StackComponent, StackDisposerComponent> _filter;
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var stack = ref _filter.Get1(entity);

                if (stack.Stack.Count == 0)
                    return;

                GameObject disposable = (GameObject)stack.Stack.Pop();
                Object.Destroy(disposable);
            }
        }
    }
}
