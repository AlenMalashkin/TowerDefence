using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifetime;

    private int _damage;
    
    private void Update()
    {
        transform.Translate(speed * Vector3.forward * Time.deltaTime);
        
        CountLifetime();
        
        if (LifetimeIsUp())
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.TryGetComponent(out EnemyHealth enemy))
        {
            enemy.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }

    public void SetProjectileDamage(int damage)
    {
        _damage = damage;
    }

    private void CountLifetime() =>
        lifetime -= Time.deltaTime;

    private bool LifetimeIsUp() =>
        lifetime <= 0;
}
