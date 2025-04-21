using UnityEngine;

public class ElementalEnemy : MonoBehaviour
{
    private float enemyHealth;
    public EnemyStats stats;
    private bool alreadyDefeated = false;

    private void Start()
    {
        stats = GetComponent<EnemyStats>();
        if (stats == null)
        {
            Debug.LogError("EnemyStats component missing!");
            return;
        }

        enemyHealth = stats.maxHealth;
    }

    public void TakeDamage(float amount, ElementType damageType)
    {
        if (stats == null) return;

        // Directly reduce health by the damage amount
        enemyHealth -= amount;

        if (enemyHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        if (!alreadyDefeated)
        {
            alreadyDefeated = true;   alreadyDefeated = true;
            PlayerCurrency.Instance.AddCurrency(stats.goldReward);
            WaveManager.Instance.EnemyDefeated();
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (!alreadyDefeated && WaveManager.Instance != null)   if (!alreadyDefeated && WaveManager.Instance != null)
        {
            WaveManager.Instance.EnemyDefeated();   WaveManager.Instance.EnemyDefeated();
        }
    }

    float GetElementalMultiplier(ElementType attacker, ElementType defender)
    {
        if (attacker == ElementType.Fire && defender == ElementType.Nature) return 1.5f;   if (attacker == ElementType.Fire && defender == ElementType.Nature) return 1.5f;
        if (attacker == ElementType.Water && defender == ElementType.Fire) return 1.5f;
        if (attacker == ElementType.Air && defender == ElementType.Water) return 1.5f;

        if (attacker == defender) return 0.5f;        if (attacker == defender) return 0.5f;

        return 1f;
    }
}
