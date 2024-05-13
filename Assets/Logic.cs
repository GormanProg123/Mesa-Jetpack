using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Logic : MonoBehaviour
{
    public Text scoreText;
    public TextMeshProUGUI coinCountText;
    public TextMeshProUGUI FinalCoin;
    public TextMeshProUGUI best;
    public TextMeshProUGUI gameOverScoreText;

    public float score;
    public float scoreIncreaseSpeed = 0.01f;
    public GameObject coinPrefab;
    private GordonScript gordonScript;
    private int coinCount = 0;
    private float bestScore;

    void Start()
    {
        gordonScript = FindObjectOfType<GordonScript>();
        bestScore = PlayerPrefs.GetFloat("BestScore", 0);
        UpdateBestScoreText();
    }

    void Update()
    {
        UpdateScoreText(scoreIncreaseSpeed * Time.deltaTime);
    }

    public void UpdateScoreText(float scoreToAdd)
    {
        score += Mathf.RoundToInt(scoreToAdd);
        scoreText.text = score.ToString() + "m";
    }

    public void UpdateCoinCount(int amount)
    {
        coinCount += amount;
        coinCountText.text = coinCount.ToString();
    }

    public void UpdateCoinCountText()
    {
        coinCountText.text = coinCount.ToString();
    }


    public void GameOver()
    {
        gameOverScoreText.ForceMeshUpdate();
        gameOverScoreText.text = score.ToString() + "m";
        UpdateCoinCountText();
        

        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetFloat("BestScore", bestScore);
            UpdateBestScoreText();
        }

        FinalCoin.text = coinCountText.text;
    }


    void UpdateBestScoreText()
    {
        best.text = "BEST: " + bestScore.ToString() + "m";
    }
}