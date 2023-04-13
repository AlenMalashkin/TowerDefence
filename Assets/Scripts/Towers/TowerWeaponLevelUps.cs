public class TowerWeaponLevelUps
{
    private float _weaponCooldown = 1.1f;
    private int _weaponDamage = 1;

    public float GetWeaponCooldown(int level)
    {
        for (int i = 0; i < level; i++)
        {
            _weaponCooldown -= 0.1f;
        }
        
        return _weaponCooldown;
    }

    public int GetWeaponDamage(int level)
    {
        for (int i = 0; i < level; i++)
        {
            _weaponDamage += 1;
        }
        
        return _weaponDamage;
    }
}
