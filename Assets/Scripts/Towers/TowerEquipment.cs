using System.Collections.Generic;
using UnityEngine;

public class TowerEquipment : MonoBehaviour
{
    [SerializeField] private TowerWeapon[] towerWeapons;
    public TowerWeapon[] TowerWeapons => towerWeapons;
    
    public List<int> GetActiveWeapons()
    {
        var activeTowerWeapons = new List<int>();
        
        for (var i = 0; i < towerWeapons.Length; i++)
        {
            if (towerWeapons[i].gameObject.activeInHierarchy)
                activeTowerWeapons.Add(i);
        }

        return activeTowerWeapons;
    }

    public void SetTowerWeaponActive(int index)
    {
        towerWeapons[index].gameObject.SetActive(true);
    }
}
