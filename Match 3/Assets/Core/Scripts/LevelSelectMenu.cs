using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectMenu : MonoBehaviour
{
    SaveLoad _saveLoad = new SaveLoad();
    [SerializeField] private string mainMenu = "Main Menu";

    [SerializeField] private Button[] _buttons;
    [Header("--- LEVEL BUTTONS ---")]
    [SerializeField] private Sprite _passiveLevel;
    [SerializeField] private Sprite _currentLevel;
    [SerializeField] private Sprite _completedLevel;
    

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
}
