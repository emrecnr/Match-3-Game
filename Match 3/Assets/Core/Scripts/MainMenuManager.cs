using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private SaveLoad _saveLoad = new SaveLoad();
    [SerializeField] private TMP_Text _healthText;

    private void Start()
    {
        _saveLoad.CheckSet();
        _healthText.text = _saveLoad.LoadInteger("Coin").ToString();
       
    }
    public void StartGame(string levelToLoad)
    {


    }
    public void QuitGame()
    {

    }
    public void GoToMapMenu(string levelToLoad)
    {
        SceneManager.LoadScene(levelToLoad);
    }
    public void GoToProfileMenu(string levelToLoad)
    {

    }
    public void ButtonProcess(string value)
    {
        switch (value)
        {
            case "Start Game":
                int sceneIndex = _saveLoad.LoadInteger("Last Level");
                SceneManager.LoadScene(sceneIndex);
                break;
            case "Level Select Menu":
            case "Profile Menu":
            case "Prize Wheel":
            case "Boosters Menu":
            case "Scores Menu":
                SceneManager.LoadScene(value);
                break;            

        }
    }
}



