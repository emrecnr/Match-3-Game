using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    //[SerializeField] private string levelToLoad;
    private SaveLoad _saveLoad = new SaveLoad();
    [SerializeField] private TMP_Text _healthText;

    private void Start()
    {
        _saveLoad.CheckSet();
        _healthText.text = _saveLoad.LoadInteger("Coin").ToString();
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
