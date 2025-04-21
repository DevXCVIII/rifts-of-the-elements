using UnityEngine;
using UnityEngine.UI;

public class TowerUIManager : MonoBehaviour
{
    public GameObject towerPanel;
    public Text damageText;
    public Text speedText;
    public Text rangeText;

    public Button upgradeDamageButton;
    public Button upgradeSpeedButton;
    public Button upgradeRangeButton;

    private ElementalTower currentTower;

    public static TowerUIManager Instance;

    void Awake()
    {
        Instance = this;
        towerPanel.SetActive(false);
    }

    public void ShowPanel(ElementalTower tower)
    {
        Debug.Log($"Showing panel for tower: {tower.gameObject.name}");
        currentTower = tower;
        UpdateStats();
        towerPanel.SetActive(true);

        upgradeDamageButton.onClick.RemoveAllListeners();
        upgradeSpeedButton.onClick.RemoveAllListeners();
        upgradeRangeButton.onClick.RemoveAllListeners();

        upgradeDamageButton.onClick.AddListener(() => tower.UpgradeStat("Damage"));
        upgradeSpeedButton.onClick.AddListener(() => tower.UpgradeStat("Speed"));
        upgradeRangeButton.onClick.AddListener(() => tower.UpgradeStat("Range"));
    }

    public void HidePanel()
    {
        towerPanel.SetActive(false);
        currentTower = null;
    }

    public void UpdateStats()
    {
        if (currentTower == null) return;

        damageText.text = $"Damage: {currentTower.damage}";
        speedText.text = $"Attack Speed: {currentTower.attackSpeed:F1}";
        rangeText.text = $"Range: {currentTower.range}";

        upgradeDamageButton.GetComponentInChildren<Text>().text = $"Upgrade Damage ({currentTower.upgradeCost})";
        upgradeSpeedButton.GetComponentInChildren<Text>().text = $"Upgrade Attack Speed ({currentTower.upgradeCost})";
        upgradeRangeButton.GetComponentInChildren<Text>().text = $"Upgrade Range ({currentTower.upgradeCost})";
    }
}
