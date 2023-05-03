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
    public TowerHealth TowerHealth => towerHealth;
    
    private void OnEnable()
    {
        towerHealth.OnTowerHealthValueChangedEvent += OnTowerHealthValueChanged;
    }

    private void OnDisable()
    {
        towerHealth.OnTowerHealthValueChangedEvent -= OnTowerHealthValueChanged;
    }

    private void OnTowerHealthValueChanged(int health)
    {
        if (health <= 0)
            DestroyTower();
        
        lifebar.UpdateLifebar(health, towerHealth.MaxTowerHealth);
    }

    private void DestroyTower()
    {
        OnTowerDestroyedEvent?.Invoke(type);
    }

    public void TakeDamage(int damage)
    {
        towerHealth.TakeDamage(damage);
    }
}
