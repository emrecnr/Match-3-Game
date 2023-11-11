using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoosterShopMenu : MonoBehaviour
{
    [SerializeField] private List<Booster> _boosters;
    [SerializeField] private TMP_Text[] _priceTexts;
    [SerializeField] private TMP_Text[] _scoreHealTexts;
    [SerializeField] private GameObject _notEnoughCoin;

    private SaveLoad _saveLoad = new SaveLoad();

    private void Start()
    {
        SetTexts();
    }
    public void BuyBoosters(string boosterName)
    {
        int coin = _saveLoad.LoadInteger("Coin");
        Booster boosterToBuy = _boosters.Find(booster => booster.boosterName == boosterName);
        if (boosterToBuy != null && coin >=boosterToBuy.boosterPrice)
        {
            coin-=boosterToBuy.boosterPrice;
            _saveLoad.SaveInteger(boosterName, _saveLoad.LoadInteger(boosterName)+1);
        }
        else
        {
            ButtonProcess("Not Enough Open");
        }
    }
    private void SetTexts()
    {

        for (int i = 0; i < _priceTexts.Length; i++)
        {
           
            _priceTexts[i].text = _boosters[i].boosterPrice.ToString();
            
        }
        _scoreHealTexts[0].text = _saveLoad.LoadInteger("Heal").ToString();
        _scoreHealTexts[1].text = _saveLoad.LoadInteger("Coin").ToString();
    }
    public void ButtonProcess(string value)
    {
        switch (value)
        {
            case "Main Menu":
                SceneManager.LoadScene(value);
                break;
            case "Coins Shop Menu":
                SceneManager.LoadScene(value);
                break;
            case "Not Enough Open":
                _notEnoughCoin.SetActive(true);
                break;
            case "Not Enough Close":
                _notEnoughCoin.SetActive(false);
                break;
            case "Watch Video":
                //TODO: Video AD
                break;
        }

    }
}
