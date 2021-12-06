using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    private GameManager m_Game;

    private void Awake()
    {
        m_Game = GameManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(m_Game.LevelsManager.AllEnemiesDead())
            {
                m_Game.LevelsManager.NextLevel();
            }
        }
    }
}
