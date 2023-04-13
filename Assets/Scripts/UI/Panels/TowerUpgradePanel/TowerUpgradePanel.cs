using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class TowerUpgradePanel : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private TowerWeaponUpgradeButton[] weaponBuyButtons;
    [SerializeField] private TowerRegenerate _towerRegenerate;
    [SerializeField] private GameObject panel;
    
    private TowerEquipment _towerEquipment;
    private TowerHealth _towerHealth;
    
    private void OnEnable()
    {
        closeButton.onClick.AddListener(ClosePanel);
    }

    private void OnDisable()
    {
        closeButton.onClick.RemoveListener(ClosePanel);
    }

    public void Init(List<int> towerWeapons, TowerEquipment towerEquipment, TowerHealth towerHealth)
    {
        _towerEquipment = towerEquipment;
        _towerHealth = towerHealth;
        
        for (int i = 0; i < weaponBuyButtons.Length; i++)
        {
            if (towerWeapons.Contains(i))
            {
                weaponBuyButtons[i].boughtState = TowerWeaponBoughtState.Bought;
            }
            else
            {
                weaponBuyButtons[i].boughtState = TowerWeaponBoughtState.NotBought;
            }
            
            weaponBuyButtons[i].Init(_towerEquipment);
        }

        _towerRegenerate.Init(_towerHealth);
    }

    public void OpenPanel()
    {
        Time.timeScale = 0;
        panel.SetActive(true);
    }

    private void ClosePanel()
    {
        Time.timeScale = 1;
        panel.SetActive(false);
    }
}
