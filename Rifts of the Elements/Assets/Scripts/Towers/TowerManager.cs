using UnityEngine;
using System.Collections.Generic;

public class TowerManager : MonoBehaviour
{
    public static TowerManager Instance; // Singleton reference
    public GameObject selectedTowerPrefab; // The currently selected tower prefab
    public GameObject fireTowerPrefab;
    public GameObject waterTowerPrefab;
    public GameObject airTowerPrefab;
    public GameObject natureTowerPrefab;
    public GameObject neutralTowerPrefab;

    public Dictionary<GameObject, int> towerCosts = new Dictionary<GameObject, int>();

    void Start()
    {
        Debug.Log("Initializing tower costs...");

        if (fireTowerPrefab == null) Debug.LogError("fireTowerPrefab is not assigned!");
        if (waterTowerPrefab == null) Debug.LogError("waterTowerPrefab is not assigned!");
        if (airTowerPrefab == null) Debug.LogError("airTowerPrefab is not assigned!");
        if (natureTowerPrefab == null) Debug.LogError("natureTowerPrefab is not assigned!");
        if (neutralTowerPrefab == null) Debug.LogError("neutralTowerPrefab is not assigned!");

        towerCosts.Add(fireTowerPrefab, 40);
        towerCosts.Add(waterTowerPrefab, 30);
        towerCosts.Add(airTowerPrefab, 35);
        towerCosts.Add(natureTowerPrefab, 25);
        towerCosts.Add(neutralTowerPrefab, 20);

        Debug.Log("Tower costs initialized successfully.");
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Duplicate TowerManager instance detected. Destroying the new instance.");
            Destroy(gameObject); // Ensure there is only one instance of TowerManager
        }
        else
        {
            Debug.Log("TowerManager instance initialized.");
            Instance = this;
        }
    }

    public void SelectTower(GameObject towerPrefab)
    {
        selectedTowerPrefab = towerPrefab;
    }
}
