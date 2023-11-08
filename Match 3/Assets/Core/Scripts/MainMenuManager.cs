using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    //[SerializeField] private string levelToLoad;
    private SaveLoad _saveLoad = new SaveLoad();

    private void Start()
    {
        _saveLoad.CheckSet();
    }
    public void StartGame(string levelToLoad)
    {
        int sceneIndex = _saveLoad.LoadInteger("Last Level");
        SceneManager.LoadScene(sceneIndex);
    }
    public void QuitGame()
    {

    }
    public void GoToMapMenu(string levelToLoad)
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
