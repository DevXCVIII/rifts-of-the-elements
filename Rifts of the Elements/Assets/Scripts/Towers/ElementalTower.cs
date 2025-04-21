using UnityEngine;

public class ElementalTower : MonoBehaviour
{
    public ElementType elementType;
    public float damage = 10f;
    public float attackSpeed = 1f;
    public float range = 5f;
    public int towerLevel = 1;
    public int upgradeCost = 50;

    private bool isSelected = false;
    [SerializeField] private GameObject rangeIndicator;

    private Transform targetEnemy;
    private float lastAttackTime;

    void Start()
    {
        rangeIndicator = transform.Find("RangeIndicator")?.gameObject;

        if (rangeIndicator != null)
        {
            rangeIndicator.SetActive(false);
        }
        else
        {
            Debug.LogWarning("RangeIndicator not found on " + gameObject.name);
        }
    }

    void Update()
    {
        if (targetEnemy != null)
        {
            // Attack the target if the cooldown has passed
            if (Time.time - lastAttackTime >= attackSpeed)
            {
                Attack(targetEnemy);
            }

            // Check if the target is still in range
            float distanceToTarget = Vector3.Distance(transform.position, targetEnemy.position);
            if (distanceToTarget > range)
            {
                targetEnemy = null; // Lose target if out of range
            }
        }
        else
        {
            // Search for a new target
            SearchForTarget();
        }

        // Update the range indicator visibility
        if (rangeIndicator != null)
        {
            rangeIndicator.SetActive(isSelected);
            float rangeScale = range; // Assuming 5 is the default range for your indicator
            rangeIndicator.transform.localScale = new Vector3(rangeScale, 1, rangeScale);
        }
    }

    void OnMouseDown()
    {
        Debug.Log($"Tower clicked: {gameObject.name}");

        if (isSelected)
        {
            DeselectTower();
        }
        else
        {
            foreach (var tower in FindObjectsByType<ElementalTower>(FindObjectsSortMode.None))
            {
                if (tower != this)
                    tower.DeselectTower();
            }

            SelectTower();
        }
    }

    public void SelectTower()
    {
        Debug.Log($"Selecting tower: {gameObject.name}");
        isSelected = true;
        rangeIndicator.SetActive(true);
        TowerUIManager.Instance.ShowPanel(this);
    }

    public void DeselectTower()
    {
        Debug.Log($"Deselecting tower: {gameObject.name}");
        isSelected = false;
        rangeIndicator.SetActive(false);
        TowerUIManager.Instance.HidePanel();
    }

    private float GetElementalMultiplier(ElementType attacker, ElementType defender)
    {
        if (attacker == ElementType.Fire && defender == ElementType.Nature) return 2f; // Fire > Nature
        if (attacker == ElementType.Nature && defender == ElementType.Water) return 2f; // Nature > Water
        if (attacker == ElementType.Water && defender == ElementType.Fire) return 2f; // Water > Fire
        if (attacker == ElementType.Air && defender == ElementType.Fire) return 2f; // Air > Fire
        return 1f; // Default multiplier
    }

    void Attack(Transform enemy)
    {
        lastAttackTime = Time.time;

        // Get the enemy's script
        ElementalEnemy enemyScript = enemy.GetComponent<ElementalEnemy>();
        if (enemyScript != null)
        {
            // Use the local GetElementalMultiplier method
            float multiplier = GetElementalMultiplier(elementType, enemyScript.stats.elementType);
            float finalDamage = damage * multiplier;

            // Apply the calculated damage to the enemy
            enemyScript.TakeDamage(finalDamage, elementType);

            // Log the damage dealt
            Debug.Log($"{gameObject.name} dealt {finalDamage} damage to {enemy.name} (Element: {enemyScript.stats.elementType})");

            // Apply any additional elemental effects
            ApplyElementalEffects(enemy);
        }

        // Optionally, play attack animation or effects here
    }

    void SearchForTarget()
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, range);
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (var collider in enemiesInRange)
        {
            if (collider.CompareTag("Enemy"))
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = collider.transform;
                }
            }
        }

        targetEnemy = closestEnemy;
    }

    public void UpgradeStat(string stat)
    {
        if (!PlayerCurrency.Instance.CanAfford(upgradeCost))
        {
            Debug.Log("Not enough gold to upgrade " + stat);
            return;
        }

        PlayerCurrency.Instance.SpendCurrency(upgradeCost);

        switch (stat)
        {
            case "Damage":
                damage += 5f;
                break;
            case "Speed":
                attackSpeed = Mathf.Max(0.2f, attackSpeed - 0.1f);
                break;
            case "Range":
                range += 0.5f;
                break;
        }

        towerLevel++;
        upgradeCost = Mathf.RoundToInt(upgradeCost * 1.5f);
        TowerUIManager.Instance.UpdateStats();
    }

    void ApplyElementalEffects(Transform enemy)
    {
        if (elementType == ElementType.Fire)
        {
            ApplyAoEEffect(enemy);
        }
        else if (elementType == ElementType.Air)
        {
            ApplySlowEffect(enemy);
        }
        else if (elementType == ElementType.Nature)
        {
            ApplyWeakenEffect(enemy);
        }
        else if (elementType == ElementType.Water)
        {
            ApplyGoldEffect(enemy);
        }
    }

    void ApplyAoEEffect(Transform enemy)
    {
        Collider[] nearbyEnemies = Physics.OverlapSphere(enemy.position, 3f); // 3f is the AoE radius
        foreach (var collider in nearbyEnemies)
        {
            if (collider.CompareTag("Enemy"))
            {
                ElementalEnemy nearbyEnemy = collider.GetComponent<ElementalEnemy>();
                if (nearbyEnemy != null)
                {
                    nearbyEnemy.TakeDamage(damage * 0.5f, elementType); // Deal 50% damage to nearby enemies
                }
            }
        }
    }

    void ApplySlowEffect(Transform enemy)
    {
        EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();
        if (enemyStats != null)
        {
            enemyStats.moveSpeed *= 0.5f; // Reduce speed by 50%
        }
    }

    void ApplyWeakenEffect(Transform enemy)
    {
        EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();
        if (enemyStats != null)
        {
            enemyStats.maxHealth -= 10f; // Reduce max health by 10
        }
    }

    void ApplyGoldEffect(Transform enemy)
    {
        EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();
        if (enemyStats != null)
        {
            enemyStats.goldReward += 5; // Increase gold reward by 5
        }
    }
}
