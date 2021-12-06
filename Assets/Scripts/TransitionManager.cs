using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    private GameManager m_Game;

    public GameObject fade;

    public GameObject fadeInstance;

    private void Awake()
    {
        m_Game = GameManager.Instance;
        instantiate();
    }

    public void instantiate()
    {

        fadeInstance = Instantiate(fade);
        SpriteRenderer renderer = fadeInstance.GetComponent<SpriteRenderer>();

        Color temp = renderer.color;

        temp.a = 0;

        renderer.color = temp;
    }

    public void blackScreen()
    {
        SpriteRenderer renderer = fadeInstance.GetComponent<SpriteRenderer>();

        Color temp = renderer.color;
        temp.a = 1;
        renderer.color = temp;
    }

    public void whiteScreen()
    {
        SpriteRenderer renderer = fadeInstance.GetComponent<SpriteRenderer>();

        Color temp = renderer.color;
        temp.a = 0;
        renderer.color = temp;
    }

    public void transitionIn(Action<bool> callback)
    {
        StartCoroutine(fadeIn(fadeInstance, callback));
    }

    public void transitionOut(Action<bool> callback)
    {
        StartCoroutine(fadeOut(fadeInstance, callback));
    }

    IEnumerator fadeIn(GameObject fadeInstance, Action<bool> callback)
    {
        SpriteRenderer renderer = fadeInstance.GetComponent<SpriteRenderer>();

        Color temp = renderer.color;

        double opacity = Math.Round(renderer.color.a, 2) * 100;
        opacity++;

        temp.a = (float)(opacity / 100);

        renderer.color = temp;

        yield return new WaitForSeconds(0.01f);

        if(opacity < 100)
        {
            StartCoroutine(fadeIn(fadeInstance, callback));
        } else
        {
            callback(true);
        }
    }

    IEnumerator fadeOut(GameObject fadeInstance, Action<bool> callback)
    {
        SpriteRenderer renderer = fadeInstance.GetComponent<SpriteRenderer>();

        Color temp = renderer.color;

        double opacity = Math.Round(renderer.color.a, 2) * 100;
        opacity--;

        temp.a = (float)(opacity / 100);

        renderer.color = temp;

        yield return new WaitForSeconds(0.01f);

        if (opacity > 0)
        {
            StartCoroutine(fadeOut(fadeInstance, callback));
        }
        else
        {
            callback(true);
        }
    }
}
