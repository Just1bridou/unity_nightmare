using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    private GameManager m_Game;

    public GameObject dialog;

    private TMPro.TextMeshProUGUI dialogText;

    private void Awake()
    {
        m_Game = GameManager.Instance;
        dialogText = dialog.GetComponent<TMPro.TextMeshProUGUI>();
    }

    public void DisplayText(string text, int timeout)
    {
        EnableText();
        StartCoroutine(displaying(text, timeout));
    }

    IEnumerator displaying(string text, int timeout)
    {
        dialogText.text = text;
        yield return new WaitForSeconds(timeout);
        if(timeout > 0)
        {
            ResetText();
        }
    }

    public void EnableText()
    {
        dialog.SetActive(true);
    }

    public void ResetText()
    {
        dialog.SetActive(false);
        dialogText.text = "";
    }
}
