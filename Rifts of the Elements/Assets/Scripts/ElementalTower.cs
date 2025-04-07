using UnityEngine;

public class ElementalTower : MonoBehaviour
{
    public float range = 5f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    public ElementType towerElement;
    public float damage = 20f;

    void Update()
    {
        fireCountdown -= Time.deltaTime;
        if (fireCountdown <= 0f)
        {
            GameObject target = FindTarget();
            if (target != null)
            {
                target.GetComponent<ElementalEnemy>().TakeDamage(damage, towerElement);
                fireCountdown = 1f / fireRate;
            }
        }
    }

    GameObject FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist <= range)
            {
                return enemy;
            }
        }
        return null;
    }
}
