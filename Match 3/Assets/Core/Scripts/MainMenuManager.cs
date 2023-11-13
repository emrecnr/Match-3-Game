using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private SaveLoad _saveLoad = new SaveLoad();
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private TMP_Text _coinText;
    [SerializeField] private AudioSource _buttonClick;

    private void Start()
    {
        _saveLoad.CheckSet();
        _healthText.text = _saveLoad.LoadInteger("Heal").ToString();
        _coinText.text = _saveLoad.LoadInteger("Coin").ToString();

    }

    public void StartGame()
    {
        int sceneIndex = _saveLoad.LoadInteger("Last Level");
        SceneManager.LoadScene(sceneIndex);
    }
    public void LoadScene(string value)
    {
        _buttonClick.Play();
        SceneManager.LoadScene(value);
    }
    
}



