using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectMenu : MonoBehaviour
{
    SaveLoad _saveLoad = new SaveLoad();
    [SerializeField] private string mainMenu = "Main Menu";
    [SerializeField] private string _levelToLoad = null;

    [SerializeField] private Button[] _buttons;
    [Header("--- LEVEL BUTTONS ---")]
    [SerializeField] private Sprite _passiveLevel;
    [SerializeField] private Sprite _currentLevel;
    [SerializeField] private Sprite _completedLevel;

    [Header("--- GOAL UI ---")]
    [SerializeField] private GameObject _infoPanel;
    [SerializeField] private GameObject[] _infoPanelImg;
    public List<LevelSelectUI> levelGoalInfo = new List<LevelSelectUI>();
    

    private void Start()
    {
        
        int currentLevel = _saveLoad.LoadInteger("Last Level") - 1;

        for (int i = 0; i < _buttons.Length; i++)
        {
            if (i + 1 < currentLevel) // Gecilmis olan leveller
            {
                _buttons[i].GetComponent<Image>().sprite = _completedLevel;
            }
            else if(i+1 == currentLevel)// Mevcut evel
            {
                _buttons[i].GetComponent<Image>().sprite = _currentLevel;
            }
            else                              // Kilitli Level
            {
                _buttons[i].GetComponent<Image>().sprite = _passiveLevel;
                _buttons[i].enabled = false;

            }
        }
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }
    public void CloseInfoPanel()
    {
        _infoPanel.SetActive(false);
    }
    public void StartLevel()
    {
        SceneManager.LoadScene(_levelToLoad);
    }
    public void OpenInfoPanel(string level)
    {
        foreach (LevelSelectUI panels in levelGoalInfo)
        {
            if (level == panels.whichLevel)
            {
                _levelToLoad = panels.whichLevel; // Yüklenecek olan leveli burada alýyoruz.
                for (int i = 0; i < panels._goals; i++)
                {
                    _infoPanelImg[i].SetActive(true);
                    _infoPanelImg[i].GetComponent<Image>().sprite = panels._goalSprite[i];
                    _infoPanelImg[i].GetComponentInChildren<TextMeshProUGUI>().text = panels._goalValue[i].ToString();
                }
                _infoPanel.SetActive(true);
            }
        }
    }

   

}
