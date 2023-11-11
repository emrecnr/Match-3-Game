using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private List<Coins> coins;
    [SerializeField] private TMP_Text[] _priceTexts;
    [SerializeField] private TMP_Text[] _scoreHealTexts;

    private SaveLoad _saveLoad = new SaveLoad();

    public void BuyBoosters(string coinName)
    {
        Booster boosterToBuy = _boosters.Find(booster => booster.boosterName == boosterName);
        if (boosterToBuy != null && coin >= boosterToBuy.boosterPrice)
        {
            coin -= boosterToBuy.boosterPrice;
            _saveLoad.SaveInteger(boosterName, _saveLoad.LoadInteger(boosterName) + 1);
        }
        else
        {
            //TODO: Coins Shop Menu
        }
    }
    private void SetTexts()
    {

        for (int i = 0; i < _priceTexts.Length; i++)
        {
           
            _priceTexts[i].text = coins[i].price.ToString();
            
        }
        _scoreHealTexts[0].text = _saveLoad.LoadInteger("Heal").ToString();
        _scoreHealTexts[1].text = _saveLoad.LoadInteger("Coin").ToString();
    }
}
