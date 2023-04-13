using UnityEngine;
using UnityEngine.UI;

public class TowerWeaponUpgradeButton : MonoBehaviour
{
    public TowerWeaponBoughtState boughtState;
    [SerializeField] private int weaponIndex;
    [SerializeField] private Text buttonText;
    [SerializeField] private Text levelText;
    [SerializeField] private Text costText;

    private int _costByUpgrade = 100;
    private int _costByBuy = 500;

    private Button _button;

    private TowerEquipment _towerEquipment;

    private void Awake()
    {
        _button = GetComponent<Button>();

        _button.onClick.AddListener(() => UpgradeOrBuyTowerWeapon(boughtState));
    }

    private void OnDisable()
    {
        _button.interactable = true;
        levelText.gameObject.SetActive(false);
        costText.gameObject.SetActive(true);
    }

    public void Init(TowerEquipment towerEquipment)
    {
        _towerEquipment = towerEquipment;
        
        UpdateUI(boughtState, _towerEquipment.TowerWeapons[weaponIndex].Level);
    }

    private void UpgradeOrBuyTowerWeapon(TowerWeaponBoughtState state)
    {
        switch (state)
        {
            case TowerWeaponBoughtState.Bought:
                UpgradeTowerWeapon();
                costText.text = _costByUpgrade * _towerEquipment
                                                 .TowerWeapons[weaponIndex]
                                                 .Level 
                                                 + "";
                break;
            case TowerWeaponBoughtState.NotBought:
                BuyTowerWeapon();
                break;
        }
    }

    private void BuyTowerWeapon()
    {
        if (Bank.Instance.SpendMoney(CurrencyType.Money, _costByBuy))
        {
            _towerEquipment.SetTowerWeaponActive(weaponIndex);
            _button.image.color = Color.cyan;
            buttonText.text = "Улучшить";
            levelText.gameObject.SetActive(true);
            levelText.text = "Уровень: " + _towerEquipment
                                           .TowerWeapons[weaponIndex]
                                           .Level;
         
            boughtState = TowerWeaponBoughtState.Bought;   
        }
    }

    private void UpgradeTowerWeapon()
    {
        var level = _towerEquipment
            .TowerWeapons[weaponIndex]
            .Level;
        
        if (Bank.Instance.SpendMoney(CurrencyType.Money, _costByUpgrade * level))
        {
            _towerEquipment
            .TowerWeapons[weaponIndex]
            .LevelUp();

            UpdateUI(boughtState, level);            
        }
    }

    private void UpdateUI(TowerWeaponBoughtState state, int level)
    {
        switch (state)
        {
            case TowerWeaponBoughtState.Bought:
                _button.image.color = Color.cyan;
                buttonText.text = "Улучшить";
                levelText.text = "Уровень: " + level;

                costText.text = _costByUpgrade * level + "";
                
                levelText.gameObject.SetActive(true);

                if (level == 10)
                {
                    levelText.text = "Макс.";
                    _button.interactable = false;
                    costText.gameObject.SetActive(false);
                }
                break;
            case TowerWeaponBoughtState.NotBought:
                _button.image.color = Color.grey;
                buttonText.text = "Купить";
                costText.text = _costByBuy + "";
                break;
        }
    }
}
