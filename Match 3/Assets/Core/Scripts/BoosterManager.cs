using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BoosterManager : MonoBehaviour
{
    [SerializeField] private List<InGameBooster> _boosters;
    [SerializeField] private List<TMP_Text> _boostersText;
    [SerializeField] private Board _board;

    private SaveLoad _saveLoad = new SaveLoad();


    public void UseBooster(string boostType)
    {
        switch (boostType)
        {
            case "Mixer":                
                UseMixerBooster(boostType,1);
                break;
            case "Bomb":
                UseBombBooster(boostType,1);
                break;
            case "Milk":
                UseMilkBooster();
                break;
            case "Exchange":
                UseExchangeBooster();
                break;
        }
    }
    private void UseMixerBooster(string key, int value)
    {
        int currentValue = 5;
        if (currentValue >0)
        {
            int usedCurrentValue = currentValue - 1; 
            Save(key,value);
            _boostersText[0].text = usedCurrentValue.ToString();
            _board.ShuffleBoard();
        }
        else
        {
            Debug.Log("Yeterli booster yok");
        }

    }
        
    private void UseMilkBooster()
    {

    }
    private void UseBombBooster(string key, int value)
    {
       List<Candy> candiesToTurnIntoBombs = new List<Candy>();
        int numberOfCandiesToTurnIntoBomb = 3;
        
        while (numberOfCandiesToTurnIntoBomb > 0)
        {
            
            int randomX = Random.Range(0, _board.width);
            int randomY = Random.Range(0, _board.height);

            Candy candy = _board._allCandies[randomX,randomY];
            if (candy!=null && !candiesToTurnIntoBombs.Contains(candy)) 
            {
                
                candiesToTurnIntoBombs.Add(candy);
                numberOfCandiesToTurnIntoBomb--;
            }
        }
        foreach (Candy candy in candiesToTurnIntoBombs)
        {
            
            Vector2Int candyPosition = candy.posIndex;
            candy.gameObject.SetActive(false);
            Candy bomb = _board.GetPooledAvailableBomb();
            if (bomb !=null)
            {

                _board.SpawnCandy(candyPosition, bomb,true);               


            }

        }
        candiesToTurnIntoBombs.Clear();
        


        //Kayýt
        int currentValue = 5;
        if (currentValue > 0)
        {
            int usedCurrentValue = currentValue - 1;
            Save(key, value);
            _boostersText[1].text = usedCurrentValue.ToString();            
        }
        

    }
    private void UseExchangeBooster()
    {

    }   
    private void Save(string key,int value)
    {
        Debug.Log(_saveLoad.LoadInteger(key));
        _saveLoad.SaveInteger(key, _saveLoad.LoadInteger(key) - value);
        Debug.Log(_saveLoad.LoadInteger(key));
    }
}
