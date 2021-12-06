using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LevelsData;

public class LevelsManager : MonoBehaviour
{
    private GameManager m_Game;
    
    public LevelsData[] levels;

    public GameObject levelContainer;

    private List<Enemy> inLifeEnemies = new List<Enemy>();

    public event EventHandler<DamageEvent> DamageToPlayer;

    public int actualLevel;
    private int maxLevel;

    private void Awake()
    {
        m_Game = GameManager.Instance;
        maxLevel = levels.Length - 1;
    }

    public void Init()
    {
        InstantiateLevel(0);
    }

    public void NextLevel()
    {
        int next = actualLevel + 1;

        if(next > maxLevel)
        {
            m_Game.GameState = GameManager.GameStateList.End;
        } else
        {
            actualLevel = next;
            InstantiateLevel(next);
        }
        
    }

    void InstantiateLevel(int number)
    {
        inLifeEnemies = new List<Enemy>();
        m_Game.ClearContainer(levelContainer);
        m_Game.ClearContainer(m_Game.EnemyManager.fireBallsContainer);
        LevelsData level = levels[number];
        GameObject gridInstance = Instantiate(level.gridMap.gameObject, levelContainer.transform);

        m_Game.PlayerManager.InstantiatePlayer(levelContainer);

       foreach(Enemies enemyStruct in level.enemiesList)
        {
            GameObject enemyObject = enemyStruct.enemyObject;

            GameObject instantiateEnemy = Instantiate(enemyObject, levelContainer.transform);
            instantiateEnemy.transform.position = new Vector3(enemyStruct.spawnX, enemyStruct.spawnY, 0);

            Enemy enemy = instantiateEnemy.GetComponent<Enemy>();
            enemy.SetValues(enemyStruct.velocity, enemyStruct.iteration);

            inLifeEnemies.Add(enemy);

            enemy.DamageToPlayer += OnDamageToPlayer;
            enemy.EnemyDie += OnEnemyDie;
        }
    }

    public bool AllEnemiesDead()
    {
        bool res = inLifeEnemies.Count > 0 ? false : true;
        return res;
    }

    private void OnEnemyDie(object sender, DamageEvent e)
    {
        Enemy enemy = e.Object.GetComponent<Enemy>();
        inLifeEnemies.Remove(enemy);

        if(inLifeEnemies.Count == 0)
        {
            m_Game.AudioManager.playShortSound("doors");
        }
    }

    private void OnDamageToPlayer(object sender, DamageEvent e)
    {
        DamageToPlayer?.Invoke(this, e);
    }

    public void DestroyLevel()
    {
        m_Game.ClearContainer(m_Game.EnemyManager.fireBallsContainer);
        m_Game.ClearContainer(levelContainer);
    }
}
