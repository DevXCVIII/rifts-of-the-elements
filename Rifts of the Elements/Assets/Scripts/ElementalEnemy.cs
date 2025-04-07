using UnityEngine;

public class ElementalEnemy : MonoBehaviour
{
    public ElementType elementType;
    public float maxHealth = 100f;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount, ElementType damageType)
    {
        float multiplier = GetElementalMultiplier(damageType, elementType);
        float finalDamage = amount * multiplier;
        currentHealth -= finalDamage;

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    float GetElementalMultiplier(ElementType attacker, ElementType defender)
    {
        // Simple logic: Fire > Earth, Water > Fire, Air > Water, etc.
        if (attacker == ElementType.Fire && defender == ElementType.Earth) return 1.5f;
        if (attacker == ElementType.Water && defender == ElementType.Fire) return 1.5f;
        if (attacker == ElementType.Air && defender == ElementType.Water) return 1.5f;
        if (attacker == ElementType.Earth && defender == ElementType.Lightning) return 1.5f;
        if (attacker == ElementType.Lightning && defender == ElementType.Air) return 1.5f;

        if (attacker == defender) return 0.5f; // resist

        return 1f; // neutral
    }
}
