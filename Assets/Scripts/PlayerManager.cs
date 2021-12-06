using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private GameManager m_Game;
    public GameObject playerPrefab;

    [Range(0,100)]
    public int baseDamage = 10;

    [Range(0, 1000)]
    public int life = 1000;

    public double variablesDamages = 10;

    public GameObject ActualPlayer { get; private set; }

    public event EventHandler<DamageEvent> DamageToEnemy;

    private void Awake()
    {
        m_Game = GameManager.Instance;

        m_Game.LevelsManager.DamageToPlayer += OnDamageToPlayer;
    }

    private void AttackHandler(object sender, EventArgs e)
    {
        m_Game.AttackAreaManager.CreateAttackArea(ActualPlayer);
    }

    public void OnDamageEnemy(object sender, DamageEvent e)
    {
        DamageToEnemy?.Invoke(this, e);
    }

    private void OnDamageToPlayer(object sender, DamageEvent e)
    {
        Player player = ActualPlayer.GetComponent<Player>();
        player.Damage(e.Damage);

        if (player.Life <= 0)
        {
            m_Game.SetEndGame();
            if (player.gameObject)
            {
                Time.timeScale = 0.2f;
                Destroy(player.gameObject);
            }
        }
    }

    public void InstantiatePlayer(GameObject levelContainer)
    {
        ActualPlayer = Instantiate(playerPrefab.gameObject, levelContainer.transform);
        ActualPlayer.transform.position = new Vector3(-1.092996f, 0.651931f, 0);

        Player player = ActualPlayer.GetComponent<Player>();
        player.Attack += AttackHandler;

        m_Game.PlayerHealthManager.UpdateBar(player.Life, life);
    }
}
