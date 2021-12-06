using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private GameManager m_Game;

    public GameObject fireBallsContainer;

    private void Awake()
    {
        m_Game = GameManager.Instance;
        m_Game.PlayerManager.DamageToEnemy += HandlerDamageEnemy;
    }

    private void HandlerDamageEnemy(object sender, DamageEvent e)
    {
        Enemy enemy = e.Object.GetComponent<Enemy>();
        enemy.Damage(e.Damage);
    }
}
