using Cinemachine;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
// тут живут все данные сцены. Было-бы неплохо регистрировать сущности на сцене через поиск по тонким монобехам, но я решил сделать так
public class SceneData
{
    public Transform PlayerSpawnPoint;
    public Camera MainCamera;
    public CinemachineVirtualCamera VirtualCamera;

    public FactoryEntityData[] Factories;
    public ItemRecieverEntityData[] ItemRecievers;
}
