using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerClickHandler : MonoBehaviour ,IPointerClickHandler
{
    [SerializeField] private TowerUpgradePanel upgradePanel;

    private Tower _tower;

    private void OnEnable() => _tower = GetComponent<Tower>();

    public void OnPointerClick(PointerEventData eventData)
    {
        upgradePanel.OpenPanel();
        upgradePanel.Init(
                        _tower.TowerEquipment.GetActiveWeapons()
                        , _tower.TowerEquipment
                        , _tower.TowerHealth);
    }
}
