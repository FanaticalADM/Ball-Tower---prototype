using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager instance;
    public static ScoreManager Instance
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

    [SerializeField]
    private GameObject scoreText;
    [SerializeField]
    private GameObject highScoreText;
    Text textScore;
    Text textHighScore;

    [SerializeField]
    private float score = 0;
    public float Score { get { return score; } set { score = value; } }
    [SerializeField]
    private float highScore;
    public float HighScore { get { return highScore; } set { highScore = value; } }
    private string highScoreFileName = "High Score";

    [SerializeField]
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.onStartGame += ScoreReset;
        textHighScore = highScoreText.gameObject.GetComponent<Text>();
        textScore = scoreText.gameObject.GetComponent<Text>();
        textScore.text = $"Score: {score}";
        highScore = PlayerPrefs.GetFloat(highScoreFileName);
        textHighScore.text = $"High Score: {highScore}";
    }

    // Update is called once per frame
    void Update()
    {
        float Tempscore = Mathf.Round(player.transform.position.y * 10);

        if (Tempscore > score)
        {
            score = Tempscore;
        }
        if (score % 10 == 0)
        {
            UpdateScore();
        }

    }

    void UpdateScore()
    {
        textScore.text = $"Score: {score}";
        if (score > highScore)
        {
            highScore = score;
            textHighScore.text = $"High Score: {highScore}";
            PlayerPrefs.SetFloat(highScoreFileName, highScore);
        }
    }

    private void ScoreReset()
    {
        Score = 0;
    }
}
