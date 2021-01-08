using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int playerLives = 5;

    public int currentScore = 0;
    public int highScore = 10000;

    [SerializeField] private bool hasLevelTimer = true;
    public float levelTimeRemaining = 500f;
    public TMP_Text timerText;
    private bool timeIsOut;


    private PlayerDeath _playerDeath;


    private void Awake()
    {
        _playerDeath = FindObjectOfType<PlayerDeath>();
    }

    private void Update()
    {
        if (hasLevelTimer)
        {
            CountdownTimer();
        }
    }

    private void CountdownTimer()
    {
        if(levelTimeRemaining > 0)
        {
            DisplayTime(levelTimeRemaining);
        }
        else
        {
            if (!timeIsOut)
            {
                _playerDeath.TimeOutDeath();
                timeIsOut = true;
            }
        }
    }

    private void DisplayTime(float levelTimeRemaining)
    {
        levelTimeRemaining -= Time.deltaTime;
        timerText.text = levelTimeRemaining.ToString();
    }
}
