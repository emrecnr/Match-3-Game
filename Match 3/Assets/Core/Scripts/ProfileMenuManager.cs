using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProfileMenuManager : MonoBehaviour
{
    private const string _mainMenu = "Main Menu";
    [SerializeField] private AudioSource _buttonAudio;
    [SerializeField] Image[] _panelButtons; // 0 - Sound 1 - Music
    [SerializeField] Sprite[] _onOffSprites; // 0 - ON  1- OFF
    private SaveLoad _saveLoad = new SaveLoad();

   private float[] _volumes = new float[2];


    private void Start()
    {
        //_saveLoad.SaveFloat("Sound", 0);
        //_saveLoad.SaveFloat("Music", 0);
        _volumes[0] = _saveLoad.LoadFloat("Sound");
        _volumes[1] = _saveLoad.LoadFloat("Music");

        for (int i = 0; i < _volumes.Length; i++)
        {
            if (_volumes[i] == 1f)
                _panelButtons[i].sprite = _onOffSprites[0];


            else
                _panelButtons[i].sprite = _onOffSprites[1];

            Debug.Log(_volumes[i]);
        }

        _buttonAudio.volume = _volumes[0];

    }
    public void PanelButtons(string button)
    {
        switch (button)
        {
            case "Save":
                Save();
                break;
            case "Cancel":
                SceneManager.LoadScene(_mainMenu);
                break;
            case "Sound":
                ToggleButtonState(0);
                break;
            case "Music":
                ToggleButtonState(1);
                break;
        }
    }
    private void ToggleButtonState(int buttonIndex)
    {
        if (buttonIndex >= 0 && buttonIndex < _panelButtons.Length)
        {
            // Butonun mevcut durumunu kontrol et
            bool isOn = _panelButtons[buttonIndex].sprite == _onOffSprites[0];

            // Duruma göre karşıt duruma geçiş yap
            _panelButtons[buttonIndex].sprite = isOn ? _onOffSprites[1] : _onOffSprites[0];
            _volumes[buttonIndex] = isOn ? 0f : 1f;
        }        
    }


    private void Save()
    {
        _saveLoad.SaveFloat("Sound", _volumes[0]);
        _saveLoad.SaveFloat("Music", _volumes[1]);
        Debug.Log("Saved!");
        _buttonAudio.volume = _saveLoad.LoadFloat("Audio");
    }
    




}
