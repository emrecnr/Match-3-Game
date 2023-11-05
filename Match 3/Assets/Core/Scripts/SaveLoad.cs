using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad : MonoBehaviour
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
}
