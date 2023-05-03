using UnityEngine;
using UnityEngine.UI;

public class DisplayCurrencyCount : MonoBehaviour
{
    public CurrencyType type;
    [SerializeField] private Text currencyAmountText;

    private void OnEnable()
    {
        Bank.Instance.OnCurrencyValueChanged += OnCurrencyValueChanged;
        PlayerPrefs.DeleteKey(Bank.Instance._currencySavePathesMap[CurrencyType.Money]);
        currencyAmountText.text = PlayerPrefs.GetInt(Bank.Instance._currencySavePathesMap[CurrencyType.Money]) + "";
    }


    private void OnDisable()
    {
        Bank.Instance.OnCurrencyValueChanged -= OnCurrencyValueChanged;
    }

    private void OnCurrencyValueChanged(CurrencyType currencyType, int amount)
    {
        if (type == currencyType)
        {
            currencyAmountText.text = amount + "";
        }
    }
}
