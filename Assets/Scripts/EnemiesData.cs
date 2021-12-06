using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Levels/EnemiesData")]
public class EnemiesData : ScriptableObject
{
    [Header("Enemies configuration")]
    public GameObject enemy;
    public float spawnX;
    public float spawnY;
}
