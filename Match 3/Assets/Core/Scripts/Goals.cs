using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Goals 
{
    public Candy.CandyType _candyType;
    
    public  Sprite _goalImg;
    public  int _goalValue;
    public Image completedImg;    
    public bool isDone = false;
   
    
    
    
}
[System.Serializable]
public class Goals_UI
{
    public Image _goalImg;
    public TMP_Text _goalValueText;


}
