using System;
using System.Collections;
using UnityEngine;

namespace Stacks
{
    [Serializable]
    public struct StackComponent //можно было-бы генерик стракт сделать для стаков с разным содержимым, но  у нас в игре пока только один тип соержимого стака
    {
        public Stack Stack;
        public StackPusherTargetView View;
        public Transform StackTransform;
        public float StackInterval;
    }
}