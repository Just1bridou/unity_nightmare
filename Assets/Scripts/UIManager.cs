using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameManager m_Game;
    public GameObject menu;
    public GameObject endScreen;
    public GameObject inGame;
    public GameObject victoryScreen;
    public GameObject pauseMenu;

    private void Awake()
    {
        m_Game = GameManager.Instance;
    }

    void Update()
    {
        switch(m_Game.GameState)
        {
            case GameManager.GameStateList.Menu:
                DisableAll();
                menu.SetActive(true);
                break;

            case GameManager.GameStateList.Intro:
                DisableAll();
                break;

            case GameManager.GameStateList.InGame:
                DisableAll();
                inGame.SetActive(true);
                break;

            case GameManager.GameStateList.GameOver:
                DisableAll();
                endScreen.SetActive(true);
                break;

            case GameManager.GameStateList.End:
                DisableAll();
                victoryScreen.SetActive(true);
                break;

            case GameManager.GameStateList.Pause:
                DisableAll();
                inGame.SetActive(true);
                pauseMenu.SetActive(true);
                break;
        }
    }

    void DisableAll()
    {
        inGame.SetActive(false);
        menu.SetActive(false);
        endScreen.SetActive(false);
        victoryScreen.SetActive(false);
        pauseMenu.SetActive(false);
    }
}
