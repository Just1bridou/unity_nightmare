using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAreaManager : MonoBehaviour
{
    private GameManager m_Game;

    public GameObject attackArea;

    public event EventHandler<DamageEvent> DamageToEnemy;

    private void Awake()
    {
        m_Game = GameManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            int percentage = m_Game.AttackBarManager.GetPlayerAttackPercentage();
            int playerDamage = m_Game.PlayerManager.baseDamage;

            int finalDamage = percentage * playerDamage / 100;

            DamageToEnemy?.Invoke(this, new DamageEvent(collision.gameObject, finalDamage));
        }
    }

    public void CreateAttackArea(GameObject player)
    {
        GameObject area = Instantiate(attackArea, player.transform);
        area.GetComponent<AttackAreaManager>().DamageToEnemy += m_Game.PlayerManager.OnDamageEnemy;
        StartCoroutine(DeleteArea(area));
    }

    IEnumerator DeleteArea(GameObject area)
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(area);
    }
}
