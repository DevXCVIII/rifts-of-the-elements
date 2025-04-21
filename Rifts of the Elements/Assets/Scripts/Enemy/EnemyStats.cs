using UnityEngine;

public enum ElementType { Neutral, Fire, Water, Air, Nature }

public class EnemyStats : MonoBehaviour
{
    public ElementType elementType;
    public float maxHealth;
    public float moveSpeed;
    public int goldReward; // 💰 Make sure this is public

    [HideInInspector] public float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }
}
