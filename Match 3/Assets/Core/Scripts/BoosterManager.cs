using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static Board;
using UnityEngine.UI;

public class BoosterManager : MonoBehaviour
{
    [SerializeField] private List<InGameBooster> _boosters;
    [SerializeField] private List<TMP_Text> _boostersText;
    [SerializeField] private Board _board;

    private SaveLoad _saveLoad = new SaveLoad();

    [Header("SWITCHER")]
    [SerializeField] public static Candy selectedCandy1 = null;
    [SerializeField] public static Candy selectedCandy2 = null;

    private bool isCandySelectionCompleted;
    private bool isSwitcherBoostUsed;
    private void Start()
    {

        RemainingBoost();
    }
    private void Update()
    {
        if (isSwitcherBoostUsed)
        {
            HandleInput();
        }
    }
    public void UseBooster(string boostType)
    {
        switch (boostType)
        {
            case "Mixer":
                UseMixerBooster(boostType, 1);
                break;
            case "Bomb":
                UseBombBooster(boostType, 1);
                break;
            case "Milk":
                UseMilkBooster();
                break;
            case "Exchange":
                UseExchangeBooster(boostType, 1);
                break;
        }
    }
    private void UseMixerBooster(string key, int value)
    {
        int currentValue = _saveLoad.LoadInteger(key);
        if (currentValue > 0)
        {
            int usedCurrentValue = currentValue - 1;
            Save(key, value);
            _boostersText[0].text = usedCurrentValue.ToString();
            _board.ShuffleBoard();
        }
        else
        {
            Debug.Log("Yeterli Mixer booster yok");
        }

    }

    private void UseMilkBooster()
    {

    }
    private void UseBombBooster(string key, int value)
    {
        int currentValue = _saveLoad.LoadInteger("Bomb");
        //Kayýt

        if (currentValue > 0)
        {
            List<Candy> candiesToTurnIntoBombs = new List<Candy>();
            int numberOfCandiesToTurnIntoBomb = 3;

            while (numberOfCandiesToTurnIntoBomb > 0)
            {

                int randomX = Random.Range(0, _board.width);
                int randomY = Random.Range(0, _board.height);

                Candy candy = _board._allCandies[randomX, randomY];

                if (candy != null && !candiesToTurnIntoBombs.Contains(candy))
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
                if (bomb != null)
                {
                    _board.SpawnCandy(candyPosition, bomb, true);
                }

            }


            candiesToTurnIntoBombs.Clear();
            //KAYIT
            int usedCurrentValue = currentValue - 1;
            Save(key, value);
            _boostersText[1].text = usedCurrentValue.ToString();
        }
        else
        {
            Debug.Log("Bomba booster mevcut deðil");
        }







    }
    private void UseExchangeBooster(string key, int value)
    {
        int currentValue = _saveLoad.LoadInteger(key);
        if (currentValue > 0)
        {
            int usedCurrentValue = currentValue - 1;
            Save(key, value);
            _boostersText[1].text = usedCurrentValue.ToString();
            _board.currentState = BoardState.wait;
            isSwitcherBoostUsed = true;
            Debug.Log(isSwitcherBoostUsed);
        }
        else
        {
            Debug.Log("Yeterli booster yok");
        }



    }

    public void HandleInput()
    {
        Vector3 targetScale = new Vector3(0.75f, 0.75f, 0f);
        if (_board.currentState == BoardState.wait && !RoundManager.isGameOver)
        {
            if (selectedCandy1 != null && selectedCandy2 != null)
            {
                SwapCandies(selectedCandy1, selectedCandy2);
            }
        }
    }

    private void SwapCandies(Candy candy1, Candy candy2)
    {
        Vector2Int candy1Pos = candy1.posIndex;
        candy1.posIndex = candy2.posIndex;
        candy2.posIndex = candy1Pos;
        Debug.Log("Baþarýlý");
        _board._allCandies[candy1.posIndex.x, candy1.posIndex.y] = candy1;
        _board._allCandies[candy2.posIndex.x, candy2.posIndex.y] = candy2;
        selectedCandy1.GetComponent<Animator>().SetBool("isSelected", false);
        selectedCandy2.GetComponent<Animator>().SetBool("isSelected", false);
        selectedCandy1 = null;
        selectedCandy2 = null;
        isSwitcherBoostUsed = false;
        _board.currentState = BoardState.move;
    }    

    private void RemainingBoost()
    {
        _boostersText[0].text = _saveLoad.LoadInteger("Mixer").ToString();
        _boostersText[1].text = _saveLoad.LoadInteger("Exchange").ToString();
        _boostersText[2].text = _saveLoad.LoadInteger("Milk").ToString();
        _boostersText[3].text = _saveLoad.LoadInteger("Bomb").ToString();
    }
    private void Save(string key, int value)
    {
        Debug.Log(_saveLoad.LoadInteger(key));
        _saveLoad.SaveInteger(key, _saveLoad.LoadInteger(key) - value);
        Debug.Log(_saveLoad.LoadInteger(key));
    }
}
