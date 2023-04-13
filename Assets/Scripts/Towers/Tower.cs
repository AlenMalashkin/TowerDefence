using System;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Action<TowerType> OnTowerDestroyedEvent;
    public TowerType type;

    [SerializeField] private TowerEquipment towerEquipment;
    [SerializeField] private TowerClickHandler towerClickHandler;
    [SerializeField] private TowerHealth towerHealth;
    [SerializeField] private Lifebar lifebar;

    public TowerEquipment TowerEquipment => towerEquipment;
    public TowerClickHandler TowerClickHandler => towerClickHandler;
    public TowerHealth TowerHealth => towerHealth;
    
    private void OnEnable()
    {
        towerHealth.OnTowerHealthValueChangedEvent += OnTowerHealthValueChanged;
    }

    private void OnDisable()
    {
        towerHealth.OnTowerHealthValueChangedEvent -= OnTowerHealthValueChanged;
        OnTowerDestroyedEvent?.Invoke(type);
    }

    private void OnDestroy()
    {
        OnTowerDestroyedEvent?.Invoke(type);
    }

    public void TakeDamage(int damage)
    {
        towerHealth.TakeDamage(damage);
    }

    private void OnTowerHealthValueChanged(int health)
    {
        lifebar.UpdateLifebar(health, towerHealth.MaxTowerHealth);
    }
}
