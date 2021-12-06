using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
    private GameManager m_Game;

    public Image healthBarReferencce;
    public Image healthBarMoving;

    private float maxBarHeight;

    private void Awake()
    {
        m_Game = GameManager.Instance;
        m_Game.LevelsManager.DamageToPlayer += OnDamageToPlayer;
        maxBarHeight = healthBarReferencce.rectTransform.sizeDelta.y;
    }

    private void OnDamageToPlayer(object sender, DamageEvent e)
    {
        Player player = e.Object.GetComponent<Player>();
        UpdateBar(player.Life, m_Game.PlayerManager.life);
    }

    public void UpdateBar(int life, int maxLife)
    {
        float lifePerCent = 100f * life / maxLife;
        float newHeight = maxBarHeight * (100f - lifePerCent) / 100f;
        healthBarMoving.rectTransform.sizeDelta = new Vector2(healthBarMoving.rectTransform.sizeDelta.x, newHeight);
    }
}
