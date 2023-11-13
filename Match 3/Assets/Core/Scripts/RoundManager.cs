using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using static Candy;

public class RoundManager : MonoBehaviour
{
    private SaveLoad _saveLoadSystem = new SaveLoad();

    [Header("--- LEVEL SETTINGS ---")]
    [SerializeField] private float roundTime = 60f;
    [SerializeField] private int roundMove = 10;
    [SerializeField] private int _scoreToFirstStar;
    [SerializeField] private int _scoreToSecondStar;
    [SerializeField] private int _scoreToThirdStar;
    public int _currentScore;
    // PROP
    public int RoundMove { get { return roundMove; } set { roundMove = value; } }
    public int ScoreToFirstStar { get { return _scoreToFirstStar; } set { _scoreToFirstStar = value; } }
    public int ScoreToSecondStar { get { return _scoreToSecondStar; } set { _scoreToSecondStar = value; } }
    public int ScoreToThirdStar { get { return _scoreToThirdStar; } set { _scoreToThirdStar = value; } }

    [SerializeField]private UIManager _uiManager;
    [SerializeField]private Board _board;

    public static bool isGameOver = false;

    

    [Header("--- GOALS PROCESS --- ")]    
    [SerializeField]private List<Goals> _goalsList = new List<Goals>();
    [SerializeField]private List<Goals_UI> _goalsUIList = new List<Goals_UI>();
    [SerializeField] private int _goalCount;
    [SerializeField] private int _goalValue;
    private int completedGoal;
    private bool hasGameOverBeenChecked = false;

    private void Start()
    {
        StartGoalProcesses();
        StartGeneralProcess();


    }
    private void Update()
    {
        if (roundTime > 0 )
        {
            
            roundTime -= Time.deltaTime;
            if (roundMove <=0)
            {
                roundTime = 0;
                isGameOver = true;
            }
        }
        int minutes = Mathf.FloorToInt(roundTime / 60);
        int seconds = Mathf.FloorToInt(roundTime % 60);
        _uiManager.timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        _uiManager.scoreText.text = _currentScore.ToString();
        if (isGameOver && !hasGameOverBeenChecked && _board.currentState == Board.BoardState.move)
        {
            if (completedGoal >=3)
            {
                WinCheck(_currentScore);
            }
            else
            {
                _uiManager.failedPanel.SetActive(true);
            }
           
            hasGameOverBeenChecked=true;
        }
    }
    private void StartGeneralProcess()
    {
        _uiManager.scoreSlider.maxValue = _scoreToThirdStar;
        _uiManager.moveCountText.text = roundMove.ToString();
    }
    private void WinCheck(int scoreValue)
    {

        _uiManager.completedPanel.SetActive(true);
                              
        if (SceneManager.GetActiveScene().buildIndex == _saveLoadSystem.LoadInteger("Last Level"))
        {
            _saveLoadSystem.SaveInteger("Last Level", _saveLoadSystem.LoadInteger("Last Level") + 1);
        }
        

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
    private void StartGoalProcesses()
    {
        for (int i = 0; i < _goalsList.Count; i++)
        {
            _goalsUIList[i]._goalImg.sprite = _goalsList[i]._goalImg;
            _goalsUIList[i]._goalValueText.text = _goalsList[i]._goalValue.ToString();
            _goalValue = _goalsList[i]._goalValue;
        }
        _goalCount = _goalsList.Count;
    }
    public void CheckGoal(int value, CandyType candyType)
    {
        foreach (var goal in _goalsList)
        {
            if (goal._candyType == candyType)
            {
                if (!goal.isDone)
                {
                    goal._goalValue -= 1;
                    if (goal._goalValue <= 0)
                    {
                        goal._goalValue = 0;
                        goal.completedImg.color = Color.green;
                        goal.isDone = true;
                        completedGoal++;

                    }                    
                }
               
                
                for (int i = 0; i < _goalsList.Count; i++)
                {
                    _goalsUIList[i]._goalValueText.text = _goalsList[i]._goalValue.ToString();                   
                }
            }
        }
    }
    
}
