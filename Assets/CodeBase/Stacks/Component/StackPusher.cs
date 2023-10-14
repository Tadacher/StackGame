using System;
using UnityEngine;

namespace Stacks
{
    [Serializable]
    public struct StackPusher
    {
        public Transform PusherTransform;
        public float Radius;
        public string TargetLayer;
    }
}