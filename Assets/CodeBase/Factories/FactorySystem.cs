using Leopotam.Ecs;
using Stacks;
using UnityEngine;

namespace Factories
{
    public class FactorySystem : IEcsRunSystem
    {
        private EcsFilter<TimerEndFlag, Factory, StackComponent> _filter;
        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref TimerEndFlag reciever = ref _filter.Get1(entity);
                Debug.Log("fact");
                if (reciever.Value == false)
                    return;

                ref Factory factory = ref _filter.Get2(entity);
                ref StackComponent stackOwner = ref _filter.Get3(entity);

                reciever.Value = false;
                GameObject product = Object.Instantiate(factory.ProductPrefab, GetNewPositionInStack(stackOwner), Quaternion.identity);
                stackOwner.Stack.Push(product);

                if(stackOwner.View.Text != null)
                    stackOwner.View.Text.text = stackOwner.Stack.Count.ToString();
            }
        }

        // да, возможно стоило определять расстояния в стопке как-то поэлегантнее
        private Vector3 GetNewPositionInStack(StackComponent owner)
        {
            return owner.StackTransform.position +
                owner.StackInterval *
                (owner.Stack.Count + 1) *
                Vector3.up;
        }
    }
}