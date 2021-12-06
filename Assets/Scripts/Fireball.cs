using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public GameManager m_Game;

    public event EventHandler<DamageEvent> DamageToPlayer;

    private void Awake()
    {
        m_Game = GameManager.Instance;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("MapCollider"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            int damage = m_Game.LevelsManager.levels[m_Game.LevelsManager.actualLevel].enemiesDamage;

            DamageToPlayer?.Invoke(this, new DamageEvent(collision.gameObject, damage));

            Destroy(gameObject);
        }
    }
}
