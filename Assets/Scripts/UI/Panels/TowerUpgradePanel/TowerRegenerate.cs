using UnityEngine;
using UnityEngine.UI;

public class TowerRegenerate : MonoBehaviour
{
    [SerializeField] private int cost;
    
    private Button _button;
    private TowerHealth _towerHealth;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(StartRegenerateTower);
    }

    public void Init(TowerHealth towerHealth)
    {
        _towerHealth = towerHealth;
    }

    private void StartRegenerateTower()
    {
        if (!_towerHealth.IsRegenerating)
            if (Bank.Instance.SpendMoney(CurrencyType.Money, cost))
                _towerHealth.Regenerate();
    }
}
