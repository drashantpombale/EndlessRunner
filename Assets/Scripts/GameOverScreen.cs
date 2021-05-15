using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public Text Score;
    public Text Coins;
    public Text time;
    public Text FinalScore;
    public Text FinalCoins;
    public Text FinalTime;
    public Button retry;
    public Button exit;

    // Start is called before the first frame update
    void Start()
    {
        Score.text = FinalScore.text;
        Coins.text = FinalCoins.text;
        time.text = FinalTime.text;
        retry.onClick.AddListener(Retry);
        exit.onClick.AddListener(Exit);
    }

    private void Exit()
    {
        Application.Quit();
    }

    private void Retry()
    {
        SceneManager.LoadScene(1);
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
