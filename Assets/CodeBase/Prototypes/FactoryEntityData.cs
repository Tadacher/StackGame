using Factories;
using Stacks;
using System;
using UnityEngine;


/// <summary>
/// Declaration of entity Components and values
/// </summary>
[Serializable]
public class FactoryEntityData
{
    public Factory Factory;
    public StackComponent StackComponent;
    public StackPusher StackPusherData;
    public Timer Timer; 
}