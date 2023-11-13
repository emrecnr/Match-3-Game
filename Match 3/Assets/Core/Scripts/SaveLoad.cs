using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SaveLoad 
{
    public void SaveString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
    }
    public void SaveInteger(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
    }
    public void SaveFloat(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
        PlayerPrefs.Save();
    }

    public string LoadString(string key)
    {
        return PlayerPrefs.GetString(key);
    }
    public int LoadInteger(string key)
    {
        return PlayerPrefs.GetInt(key);
    }
    public float LoadFloat(string key)
    {
        return PlayerPrefs.GetFloat(key);
    }

    public void CheckSet()
    {
        if (!PlayerPrefs.HasKey("Last Level"))
        {
            PlayerPrefs.SetInt("Last Level", 1);
            PlayerPrefs.SetInt("Coin", 0);
            PlayerPrefs.SetFloat("Sound", 1);
            PlayerPrefs.SetFloat("Music", 1);
            PlayerPrefs.SetInt("Bomb", 10);
            PlayerPrefs.SetInt("Mixer", 10);
            PlayerPrefs.SetInt("Exchange", 10);
            PlayerPrefs.SetInt("Milk", 10);
            PlayerPrefs.SetInt("Heal", 5);
            
            
            
        }
    }
}
