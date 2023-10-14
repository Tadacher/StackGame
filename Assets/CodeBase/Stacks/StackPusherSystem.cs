using Leopotam.Ecs;
using UnityEngine;

namespace Stacks
{
    public class StackPusherSystem : IEcsRunSystem
    {
        private EcsFilter<StackPusher> _filter;
        private Collider[] _results;
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var pusher = ref _filter.Get1(entity);
                _results = GetUnitsInTrigger(pusher);


                if (_results != null && _results.Length > 0)
                {
                    ref var initialStack = ref _filter.GetEntity(entity).Get<StackComponent>();


                    ref var targetEntity = ref _results[0].GetComponent<StackPusherTargetView>().Entity;
                    ref var targetStack = ref targetEntity.Get<StackComponent>();

                    if (initialStack.Stack.Count == 0)
                        return;
                    GameObject poppedGameobject = (GameObject)initialStack.Stack.Pop();

                    poppedGameobject.transform.parent = _results[0].transform;
                    poppedGameobject.transform.localPosition = Vector3.up * targetStack.StackInterval * (targetStack.Stack.Count + 1);
                    targetStack.Stack.Push(poppedGameobject);

                    DrawStackCount(initialStack);
                    DrawStackCount(targetStack);

                }
            }
        }

        private static void DrawStackCount(StackComponent initialStack)
        {
            if (initialStack.View.Text != null)
                initialStack.View.Text.text = initialStack.Stack.Count.ToString();
        }

        private Collider[] GetUnitsInTrigger(StackPusher pusher) =>
            Physics.OverlapSphere(pusher.PusherTransform.position, pusher.Radius, LayerMask.GetMask(pusher.TargetLayer));
    }
}
