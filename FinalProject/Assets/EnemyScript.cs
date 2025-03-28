using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public float damage = 10f;

    [SerializeField] private EnemyHealthBar healthBar;
    
    private void Awake()
    {
        healthBar = GetComponentInChildren<EnemyHealthBar>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.UpdateHealthBar(currentHealth, maxHealth); // Update health bar on start
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerScript player = other.GetComponent<PlayerScript>();

            if (!player.isInvincible)
            {
                player.TakeDamage(damage);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            ProjectileScript projectile = other.GetComponent<ProjectileScript>();
            currentHealth -= projectile.atk;

            healthBar.UpdateHealthBar(currentHealth, maxHealth);
            Debug.Log($"Enemy has {currentHealth} hp remaining.");

            if (currentHealth <= 0)
            {
                Debug.Log("Enemy has been destroyed!!");
                Destroy(gameObject);
            }
        }
    }
}
