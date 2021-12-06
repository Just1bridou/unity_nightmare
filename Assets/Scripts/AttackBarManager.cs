using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackBarManager : MonoBehaviour
{
    private GameManager m_Game;

    public Image healthBarReferencce;
    public Image healthBarMoving;

    [Range(1,3)]
    public float ratio = 1.1f;

    [Range(1f, 10f)]
    public float speed = 1f;

    private float maxBarHeight;

    private bool inUse = false;

    private float height;

    private Coroutine lastRoutine = null;

    private void Awake()
    {
        m_Game = GameManager.Instance;
        maxBarHeight = healthBarReferencce.rectTransform.sizeDelta.y;
        height = maxBarHeight;
    }

    public bool isInUse()
    {
        return inUse;
    }

    public void Attack()
    {
        if(lastRoutine != null)
        {
            StopCoroutine(lastRoutine);
        }
        height = 2f;
        lastRoutine = StartCoroutine(UpdateBar());
    }

    public IEnumerator UpdateBar()
    {
        float newHeight = height * ratio;
        float inverseHeight = maxBarHeight - newHeight;

        healthBarMoving.rectTransform.sizeDelta = Vector2.Lerp( new Vector2(healthBarMoving.rectTransform.sizeDelta.x, healthBarMoving.rectTransform.sizeDelta.y), new Vector2(healthBarMoving.rectTransform.sizeDelta.x, inverseHeight), 1);

        if (newHeight < maxBarHeight)
        {
            yield return new WaitForSeconds(speed/100);

            height = newHeight;
            lastRoutine = StartCoroutine(UpdateBar());

            m_Game.PlayerManager.variablesDamages = height;
        }
    }

    public int GetPlayerAttackPercentage()
    {
        int percentage = (int)Math.Round(m_Game.PlayerManager.variablesDamages * 100 / maxBarHeight) + 3;
        percentage = percentage > 100 ?  100 :  percentage;
        return percentage;
    }
}
