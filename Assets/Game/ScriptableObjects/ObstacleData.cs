using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Obstacle Data")]
public class ObstacleData : ScriptableObject
{
    public Material ObstacleMaterial;
    [Tag] public string ObstacleTag;
}
