using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectButton : MonoBehaviour
{
    [SerializeField] private string levelToLoad;

    [SerializeField] GameObject[] stars;

    private SaveLoad _saveLoad = new SaveLoad();
    void Start()
    {
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].SetActive(false);
        }
        CheckStars(_saveLoad.LoadInteger(levelToLoad + "_Star"));
    }
    private void CheckStars(int starValue)
    {
        
        switch (starValue)
        {
            case 1:
                stars[0].SetActive(true);
                break;
            case 2:
                stars[1].SetActive(true);
                break;
            case 3:
                stars[2].SetActive(true);
                break;
        }
    }
    public void LoadLevel()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
