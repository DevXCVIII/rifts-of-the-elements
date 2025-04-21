using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    public Material ghostMaterial;
    private GameObject previewTower;

    void Update()
    {
        if (TowerManager.Instance.selectedTowerPrefab == null)
        {
            DestroyPreview();
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Transform platform = hit.collider.transform;

            if (hit.collider.CompareTag("TowerPlatform") && platform.childCount == 0)
            {
                ShowPreview(platform.position);

                if (Input.GetMouseButtonDown(0))
                {
                    int cost = TowerManager.Instance.towerCosts[TowerManager.Instance.selectedTowerPrefab];

                    if (PlayerCurrency.Instance.CanAfford(cost))
                    {
                        PlayerCurrency.Instance.SpendCurrency(cost);
                        GameObject tower = Instantiate(TowerManager.Instance.selectedTowerPrefab, platform.position, Quaternion.identity);
                        tower.transform.SetParent(platform);
                        TowerManager.Instance.selectedTowerPrefab = null;
                        DestroyPreview();
                    }
                    else
                    {
                        PlayerCurrency.Instance.ShowInsufficientFunds(); // trigger feedback
                    }
                }

                return;
            }
        }

        DestroyPreview();
    }

    void ShowPreview(Vector3 position)
    {
        if (previewTower == null)
        {
            previewTower = Instantiate(TowerManager.Instance.selectedTowerPrefab, position, Quaternion.identity);

            foreach (Renderer r in previewTower.GetComponentsInChildren<Renderer>())
            {
                r.material = ghostMaterial;
            }
        }
        else
        {
            previewTower.transform.position = position;
        }
    }

    void DestroyPreview()
    {
        if (previewTower != null)
        {
            Destroy(previewTower);
            previewTower = null;
        }
    }
}
