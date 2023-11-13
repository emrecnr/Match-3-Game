using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RewardManager : MonoBehaviour
{
    [Header("--- REWARD --- ")]
    public Dictionary<string, Sprite> rewardSprites = new Dictionary<string, Sprite>();
    [SerializeField] private string _rewardKey = "RewardClaimed";
    [SerializeField] private Image _rewardImage;
    [SerializeField] private float rewardCooldown = 86400; // 24 saat
    [SerializeField] private List<Sprite> _spritesReward;
    [SerializeField] private Button _spinButton;
    private DateTime lastClaimTime;

    private SaveLoad _saveLoad = new SaveLoad();

    [SerializeField] private TMP_Text _healText;
    [SerializeField] private TMP_Text _coinText;
    [SerializeField] private TMP_Text _pieceText;
    void Start()
    {
        _healText.text = _saveLoad.LoadInteger("Heal").ToString();
        _coinText.text = _saveLoad.LoadInteger("Coin").ToString();
        SetSpriteDic();
        CheckRewardClaim();
    }
    private void SetSpriteDic()
    {
        rewardSprites.Add("Coin", _spritesReward[0]);
        rewardSprites.Add("Exchange", _spritesReward[1]);
        rewardSprites.Add("Heal", _spritesReward[2]);
        rewardSprites.Add("Mixer", _spritesReward[3]);
        rewardSprites.Add("Milk", _spritesReward[4]);
    }
    private void CheckRewardClaim()
    {
        string savedTime = _saveLoad.LoadString(_rewardKey);
        if (!string.IsNullOrEmpty(savedTime))
        {
            lastClaimTime = DateTime.Parse(savedTime);
            if (CanClaimReward())
            {
                ButtonInteractable(true);
            }
            else
                ButtonInteractable(false);
        }
        else
        {
            //Debug.Log("Ilk Çevirme");
            ButtonInteractable(true);
        }
    }
    private bool CanClaimReward()
    {
        TimeSpan timeSinceLastClaim = DateTime.Now - lastClaimTime;
        return timeSinceLastClaim.TotalSeconds >= rewardCooldown;
    }
    public void ClaimReward(string reward, int piece = 1)
    {
        if (CanClaimReward())
        {
            ShowReward(reward);
            Save(reward, piece);
            _pieceText.text = "+"+piece.ToString();
            _pieceText.gameObject.SetActive(true);
        }

    }
    public void ShowReward(string reward)
    {
        _rewardImage.sprite = rewardSprites[reward];
        _rewardImage.SetNativeSize();
    }
    private void Save(string key, int value)
    {
        lastClaimTime = DateTime.Now;
        _saveLoad.SaveInteger(key, _saveLoad.LoadInteger(key) + value);
        _saveLoad.SaveString(_rewardKey, lastClaimTime.ToString());
        switch (key)
        {
            case "Coin":
                _coinText.text = _saveLoad.LoadInteger("Coin").ToString();
                break;
            case "Heal":
                _healText.text = _saveLoad.LoadInteger("Heal").ToString();
                break;
        }
       
    }

    public void ButtonInteractable(bool state)
    {
        if (state)
        {
            _spinButton.interactable = true;
        }
        else
        {
            _spinButton.interactable = false;
        }
    }
}
