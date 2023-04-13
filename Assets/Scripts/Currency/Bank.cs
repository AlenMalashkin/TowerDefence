using System;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{
    public event Action<CurrencyType, int> OnCurrencyValueChanged;
    
    public static Bank Instance => _instance;
    private static Bank _instance;

    public Dictionary<CurrencyType, string> _currencySavePathesMap = new Dictionary<CurrencyType, string>()
    {
        {CurrencyType.Money, "Money"}
    };

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GetMoney(CurrencyType type, int amount)
    {
        var amountOfCurrency = PlayerPrefs.GetInt(_currencySavePathesMap[type], 0);

        amountOfCurrency += amount;

        PlayerPrefs.SetInt(_currencySavePathesMap[type], amountOfCurrency);
        OnCurrencyValueChanged?.Invoke(type, amountOfCurrency);
    }

    public bool SpendMoney(CurrencyType type, int amount)
    {
        var amountOfCurrency = PlayerPrefs.GetInt(_currencySavePathesMap[type], 0);

        if (amountOfCurrency < amount) 
            return false;
        
        amountOfCurrency -= amount;
        PlayerPrefs.SetInt(_currencySavePathesMap[type], amountOfCurrency);
        OnCurrencyValueChanged?.Invoke(type, amountOfCurrency);

        return true;
    }
}
