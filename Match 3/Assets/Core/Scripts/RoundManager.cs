using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] private float roundTime = 60f;
    public int _currentScore;
    [SerializeField]private UIManager _uiManager;

    public static bool isGameOver = false;

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
    }
}
