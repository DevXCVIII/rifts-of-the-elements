using UnityEngine;
using UnityEngine.UI;

public class ElementalEnemy : MonoBehaviour
{
    private float enemyHealth;
    public EnemyStats stats;
    private bool alreadyDefeated = false;

    public GameObject healthBarPrefab;

    private HealthBar healthBar;

    private void Start()
    {
        stats = GetComponent<EnemyStats>();
        if (stats == null)
        {
            Debug.LogError("EnemyStats component missing!");
            return;
        }

        enemyHealth = stats.maxHealth;

        if (healthBarPrefab != null)
        {
            // Set an offset to move the health bar lower (closer to the enemy's head)
            Vector3 offset = new Vector3(0, -3f, 0); // adjust Y as needed

            GameObject barObj = Instantiate(healthBarPrefab, transform.position + offset, Quaternion.identity);
            healthBar = barObj.GetComponent<HealthBar>();
            if (healthBar != null)
            {
                healthBar.Initialize(transform); // removed offset to match the method signature
                healthBar.SetHealth(enemyHealth, stats.maxHealth);
            }
        }
    }

    public void TakeDamage(float amount, ElementType damageType)
    {
        if (stats == null) return;

        enemyHealth -= amount;

        if (healthBar != null)
            healthBar.SetHealth(enemyHealth, stats.maxHealth);

        if (enemyHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        if (!alreadyDefeated)
        {
            alreadyDefeated = true;
            PlayerCurrency.Instance.AddCurrency(stats.goldReward);
            WaveManager.Instance.EnemyDefeated();
            Destroy(healthBar?.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (!alreadyDefeated && WaveManager.Instance != null)
        {
            WaveManager.Instance.EnemyDefeated();
        }
    }

    float GetElementalMultiplier(ElementType attacker, ElementType defender)
    {
        if (attacker == ElementType.Fire && defender == ElementType.Nature) return 1.5f;
        if (attacker == ElementType.Water && defender == ElementType.Fire) return 1.5f;
        if (attacker == ElementType.Air && defender == ElementType.Water) return 1.5f;
        if (attacker == defender) return 0.5f;
        return 1f;
    }
}
