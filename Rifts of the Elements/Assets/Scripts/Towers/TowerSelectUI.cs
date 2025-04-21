using UnityEngine;

public class TowerSelectUI : MonoBehaviour
{
    public GameObject fireTowerPrefab;
    public GameObject waterTowerPrefab;
    public GameObject airTowerPrefab;
    public GameObject natureTowerPrefab;
    public GameObject neutralTowerPrefab;

    public void OnFireTowerClick() {
        TowerManager.Instance.SelectTower(fireTowerPrefab);
    }

    public void OnWaterTowerClick() {
        TowerManager.Instance.SelectTower(waterTowerPrefab);
    }

    public void OnAirTowerClick() {
        TowerManager.Instance.SelectTower(airTowerPrefab);
    }

    public void OnNatureTowerClick() {
        TowerManager.Instance.SelectTower(natureTowerPrefab);
    }

    public void OnNeutralTowerClick() {
        TowerManager.Instance.SelectTower(neutralTowerPrefab);
    }
}
