using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour,IStoreListener
{
    private static IStoreController m_StoreController;
    private static IExtensionProvider m_ExtensionProvider;

   

    [SerializeField] private List<Coins> coins;
    [SerializeField] private TMP_Text[] _priceTexts;
    [SerializeField] private TMP_Text[] _scoreHealTexts;

    private SaveLoad _saveLoad = new SaveLoad();

    private void Start()
    {
        SetTexts();
        if (m_StoreController ==null)
        {
            InitializePurchasing();
        }
        
        
    }

    public void InitializePurchasing()
    {
        if (IsInitialized())
        {
            return;
        }
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        for (int i = 0; i < coins.Count; i++)
        {
            builder.AddProduct(coins[i].coin, ProductType.Consumable);
        }
        UnityPurchasing.Initialize(this, builder);


    }
    public void Buy(string value)
    {
        BuyProductID(value);
    }

    private void BuyProductID(string productID)
    {
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productID);
            if (product != null && product.availableToPurchase)
            {
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("Error");
            }
        }
        else
        {
            Debug.Log("Not Found");
        }
    }
    private bool IsInitialized()
    {
        return m_StoreController != null && m_ExtensionProvider != null;
    }
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        for (int i = 0; i < coins.Count; i++)
        {
            if (string.Equals(purchaseEvent.purchasedProduct.definition.id, coins[i].coin, StringComparison.Ordinal))
            {
                _saveLoad.SaveInteger("Coin", _saveLoad.LoadInteger("Coin") + coins[i].price);
                break;
            }
        }
        return PurchaseProcessingResult.Complete;

    }
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        m_StoreController = controller;
        m_ExtensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        throw new System.NotImplementedException();
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        throw new System.NotImplementedException();
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
    public void LoadScene(string value)
    {
        SceneManager.LoadScene(value);
    }
}
