using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Levels/LevelData")]
public class LevelsData : ScriptableObject
{
    [Header("Level configuration")]
    public GameObject gridMap;

    [Header("Enemies list")]
    public List<Enemies> enemiesList;

    [System.Serializable]
    public struct Enemies
    {
        [Header("Enemy")]
        public GameObject enemyObject;
        public float spawnX;
        public float spawnY;

        [Header("Fireballs")]
        public float velocity;
        [Range(1, 6)]
        public int iteration;
    }

    [Header("Enemies configuration")]
    public int enemiesDamage;
    public int enemiesLife;
}

