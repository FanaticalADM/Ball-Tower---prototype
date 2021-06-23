using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
        set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public enum GameStatus
    {
        Start,
        Game,
        Over
    }

    private GameStatus gameStatus;
    public GameStatus CheckGameStatus { get { return gameStatus; } }

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private bool hardcoreModeOn;
    public bool HardcoreModeOn { get { return hardcoreModeOn; } }

    public event Action onStartGame;
    public void StartGame()
    {
        if (onStartGame != null)
        {
            onStartGame();
        }

        player.SetActive(true);
        gameStatus = GameStatus.Game;
    }

    public event Action onGameOver;
    public void GameOver()
    {
        gameStatus = GameStatus.Over;
        if (onGameOver != null)
        {
            onGameOver();
        }
        player.SetActive(false);
    }

    public event Action<bool> onHardcoreModeToggle;
    public void HardcoreModeToggle()
    {
        hardcoreModeOn = !hardcoreModeOn;
        bool isHardcoreModeOn = hardcoreModeOn;
        if (onHardcoreModeToggle != null)
        {
            onHardcoreModeToggle(isHardcoreModeOn);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameStatus = GameStatus.Start;
        hardcoreModeOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y < SpawnManager.Instance.Hight-9.9f)
        {
            GameOver();
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}