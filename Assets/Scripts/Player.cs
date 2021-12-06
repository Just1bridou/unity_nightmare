using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameManager m_Game;

    public event EventHandler<EventArgs> Attack;

    public int Life { get; private set; }

    private void Awake()
    {
        m_Game = GameManager.Instance;
        Life = m_Game.PlayerManager.life;
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {

            Animator animator = GetComponent<Animator>();
            animator.SetTrigger("Attack");

            HandleAttack();

            m_Game.AudioManager.playShortSound("sword");
           // m_Game.AudioManager.playSound("sword");

            m_Game.AttackBarManager.Attack();
        }
    }

    private void HandleAttack()
    {
        Attack?.Invoke(this, EventArgs.Empty);
    }

    public void Damage(int damage)
    {
        Life = Life - damage;
    }

}
