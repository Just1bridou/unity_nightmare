using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerManager PlayerManager { get; private set; }

    public EnemyManager EnemyManager { get; private set; }

    public LevelsManager LevelsManager { get; private set; }

    public PlayerHealthManager PlayerHealthManager { get; private set; }

    public AttackAreaManager AttackAreaManager { get; private set; }

    public AttackBarManager AttackBarManager { get; private set; }

    public UIManager UIManager { get; private set; }

    public IntroductionManager IntroductionManager { get; private set; }

    public AudioManager AudioManager { get; private set; }

    public TransitionManager TransitionManager { get; private set; }

    public DialogManager DialogManager { get; private set; }

    public enum GameStateList {Menu, Pause, Intro, InGame, GameOver, End };

    public GameStateList GameState = GameStateList.Menu;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else if (Instance != this)
        {
            Destroy(gameObject);
        }

        EnemyManager = GetComponent<EnemyManager>();
        LevelsManager = GetComponent<LevelsManager>();
        PlayerManager = GetComponent<PlayerManager>();
        PlayerHealthManager = GetComponent<PlayerHealthManager>();
        AttackAreaManager = GetComponent<AttackAreaManager>();
        AttackBarManager = GetComponent<AttackBarManager>();
        UIManager = GetComponent<UIManager>();
        IntroductionManager = GetComponent<IntroductionManager>();
        AudioManager = GetComponent<AudioManager>();
        TransitionManager = GetComponent<TransitionManager>();
        DialogManager = GetComponent<DialogManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("r") && GameState == GameStateList.GameOver)
        {
            Restart();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameState == GameStateList.Pause)
            {
                PauseResume();
            } else if (GameState == GameStateList.InGame)
            {
                Pause();
            }
        }
    }

    public void StartIntro()
    {
        DialogManager.DisplayText("23th NOVEMBER  9:00 pm", 0);
        GameState = GameStateList.Intro;
        TransitionManager.blackScreen();
        TransitionManager.transitionOut((res) => { });
        IntroductionManager.startIntroduction();
    }

    private void TimerCompletedHandler(object sender, EventArgs e)
    {
        StopGame();
    }

    public void SetEndGame()
    {
        GameState = GameStateList.GameOver;
    }

    public void StartGame()
    {
        DialogManager.ResetText();
        TransitionManager.transitionOut((res) => { });
        AudioManager.stopSound("musicHouse");
        AudioManager.playSound("musicDungeon", 0.7f, true);
        GameState = GameStateList.InGame;
        LevelsManager.Init();
    }

    private void StopGame()
    {
        GameState = GameStateList.GameOver;
    }

    public void Restart()
    {
        AudioManager.stopSound("musicDungeon");
        AudioManager.playSound("musicDungeon", 0.7f, true);

        GameState = GameStateList.InGame;
        LevelsManager.Init();
        PlayerHealthManager.UpdateBar(PlayerManager.ActualPlayer.GetComponent<Player>().Life, PlayerManager.life);
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        Time.timeScale = 0;
        GameState = GameManager.GameStateList.Pause;
    }

    public void PauseResume()
    {
        Time.timeScale = 1f;
        GameState = GameManager.GameStateList.InGame;
    }

    public void DisplayMenu()
    {
        GameState = GameManager.GameStateList.Menu;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ClearContainer(GameObject container)
    {
        foreach (Transform child in container.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
