using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    private static GameObject instance;
    private AudioSource musicSource;
    private SaveLoad _saveLoad = new SaveLoad();
    float volume;
    private void Start()
    {
        musicSource = GetComponent<AudioSource>();
       
        DontDestroyOnLoad(this.gameObject);
        if (instance == null)
        {
            instance = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        volume = _saveLoad.LoadFloat("Music");
        musicSource.volume = volume;
    }
}
