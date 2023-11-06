using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour
{
    [Header("--- LEVEL SETTINGS ---")]
    [SerializeField] private float roundTime = 60f;
    [SerializeField] private int _scoreToFirstStar;
    [SerializeField] private int _scoreToSecondStar;
    [SerializeField] private int _scoreToThirdStar;

    public int _currentScore;
    [SerializeField]private UIManager _uiManager;

    public static bool isGameOver = false;

    private SaveLoad _saveLoadSystem = new SaveLoad();
    private void Update()
    {
        if (roundTime > 0)
        {
            roundTime -= Time.deltaTime;
            if (roundTime <= 0)
            {
                roundTime = 0;
                isGameOver = true;
            }
        }
        int minutes = Mathf.FloorToInt(roundTime / 60);
        int seconds = Mathf.FloorToInt(roundTime % 60);
        _uiManager.timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        _uiManager.scoreText.text = _currentScore.ToString();
        if (isGameOver)
        {
            WinCheck(_currentScore);
        }
    }

    private void WinCheck(int scoreValue)
    {
        //_uiManager.completedPanel.SetActive(true);

        if (scoreValue >= _scoreToThirdStar)
        {

           _saveLoadSystem.SaveInteger(SceneManager.GetActiveScene().name + "_Star", 3);
        }
        if (scoreValue >= _scoreToSecondStar)
        {
            _saveLoadSystem.SaveInteger(SceneManager.GetActiveScene().name + "_Star", 2);
        }

        if (scoreValue >= _scoreToFirstStar)
        {
            _saveLoadSystem.SaveInteger(SceneManager.GetActiveScene().name + "_Star", 1);

        }
    }
}
