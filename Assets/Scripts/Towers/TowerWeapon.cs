using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerWeapon : MonoBehaviour
{
    [SerializeField] private TriggerObserver triggerObserver;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private AudioClip shotSound;
    [SerializeField] private float shootCooldown;
    [SerializeField] private int damage;

    public int Level => _level;
    
    private int _level = 1;
    private List<Enemy> _enemies;
    private Enemy _target;
    private float _currentShootCooldown;
    private bool _isShooting;

    private void OnEnable()
    {
        _enemies = new List<Enemy>();
        
        triggerObserver.OnTriggerEnteredEvent += OnTriggerEntered;
    }

    private void OnDisable()
    {
        triggerObserver.OnTriggerEnteredEvent -= OnTriggerEntered;
    }

    public void LevelUp()
    {
        if (_level < 10)
            _level++;
        
        var weaponLevelUps = new TowerWeaponLevelUps();
        SetUpgradedWeaponStats(weaponLevelUps.GetWeaponCooldown(_level), weaponLevelUps.GetWeaponDamage(_level));
    }
    
    private void SetUpgradedWeaponStats(float upgradedSootCooldown, int upgradedDamage)
    {
        shootCooldown = upgradedSootCooldown;
        damage = upgradedDamage;
    }

    private void Update()
    {
        if (_enemies.Count != 0)
        {
            _target = _enemies[0];

            if (_target.IsDied)
            {
                foreach (var enemy in _enemies)
                {
                    if (!enemy.IsDied)
                    {
                        _target = enemy;
                        break;
                    }
                }
            }

            if (!_target.IsDied)
            {
                transform.LookAt(_target.transform);
                            
                CountCooldown();
            
                if (CooldownIsUp())
                    Shoot();     
            }
                           
        }
    }

    private void OnTriggerEntered(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            _enemies.Add(enemy);
            enemy.EnemyKilledEvent.AddListener(EnemyKilled);
        }
    }

    private void EnemyKilled(Enemy enemy)
    {
        _enemies.Remove(enemy);
    }

    private void CountCooldown() =>
        _currentShootCooldown -= Time.deltaTime;
    
    private bool CooldownIsUp() =>
        _currentShootCooldown <= 0;
    
    private void Shoot()
    {
        _currentShootCooldown = shootCooldown;
        var projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation, projectileSpawnPoint);
        projectile.SetProjectileDamage(damage);
        projectile.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        SoundPlayer.Instance.PlaySfx(shotSound);
    }
}
