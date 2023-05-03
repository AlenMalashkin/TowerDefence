 using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public UnityEvent<Enemy> EnemyKilledEvent = new UnityEvent<Enemy>();
    public EnemyType type;

    [Header("Depends")] [SerializeField] private Animator animator;
    [SerializeField] private TriggerObserver triggerObserver;
    [SerializeField] private EnemyAttack enemyAttack;
    [SerializeField] private EnemyMove enemyMove;
    [SerializeField] private EnemyHealth enemyHealth;
    [SerializeField] private Lifebar lifebar;

    [Header("Particulars")] 
    [SerializeField] private AudioClip dieSfx;
    [SerializeField] private int amountOfMoney;

    public bool IsDied => _died;
    
    private Tower _tower;
    private bool _died;

    public void SetTargetTower(Tower tower)
    {
        if (!_died)
        {
             animator.SetBool("Run", true);
             _tower = tower;   
        }
    }
    
    public void SetEnemyStats(int damage, int maxHealth, float attackRate)
    {
        enemyAttack.Damage = damage;
        enemyAttack.AttackCooldown = attackRate;
        enemyHealth.MaxHealth = maxHealth;
    }

    private void OnEnable()
    {
        triggerObserver.OnTriggerEnteredEvent += OnTriggerEntered;
        triggerObserver.OnTriggerExitedEvent += OnTriggerExited;
        enemyHealth.OnEnemyHealthValueChanged += OnEnemyHealthChanged;
    }

    private void OnDisable()
    {
        triggerObserver.OnTriggerEnteredEvent -= OnTriggerEntered;
        triggerObserver.OnTriggerExitedEvent -= OnTriggerExited;
        enemyHealth.OnEnemyHealthValueChanged -= OnEnemyHealthChanged;
    }

    private void Update()
    {
        if (!_died)
        {
             enemyMove.Move(_tower.transform.position);
        }
    }
    
    private void OnTriggerEntered(Collider other)
    {
        if (other.TryGetComponent(out Tower tower) && tower.type == _tower.type)
        {
            animator.SetBool("Run", false);
            animator.SetBool("Attack", true);
            enemyAttack.EnableAttack(tower);
        }
    }

    private void OnTriggerExited(Collider other)
    {
        if (other.TryGetComponent(out Tower tower) && tower.type == _tower.type)
        { 
            animator.SetBool("Attack", false);
            animator.SetBool("Run", true);
            enemyAttack.DisableAttack();
            enemyMove.Move(_tower.transform.position); 
        }
    }

    private void OnEnemyHealthChanged(int health)
    {
        if (health <= 0)
            Die();
        
        lifebar.UpdateLifebar(health, enemyHealth.MaxHealth);
    }
    
    private void Die()
    {
        _died = true;
        GetComponent<Collider>().enabled = false;
        animator.SetBool("Attack", false);
        animator.SetBool("Run", false);
        animator.SetBool("Died", true);
        enemyMove.Stop();
        SoundPlayer.Instance.PlaySfx(dieSfx);
    }

    private void OnDied()
    { 
        Bank.Instance.GetMoney(CurrencyType.Money, amountOfMoney);
        EnemyKilledEvent.Invoke(this);
        Destroy(gameObject);
    }
}
