using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string levelToLoad;

    public void StartGame()
    {
        SceneManager.LoadScene(levelToLoad);
    }
    public void QuitGame()
    {

    }
}
