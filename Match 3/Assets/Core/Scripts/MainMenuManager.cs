using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    //[SerializeField] private string levelToLoad;

    public void StartGame(string levelToLoad)
    {
        SceneManager.LoadScene(levelToLoad);
    }
    public void QuitGame()
    {

    }
    public void GoToMapMenu(string levelToLoad)
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
