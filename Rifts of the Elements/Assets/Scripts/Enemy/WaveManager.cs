using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    public GameObject neutralElezarPrefab;
    public GameObject fireElezarPrefab;
    public GameObject waterElezarPrefab;
    public GameObject airElezarPrefab;
    public GameObject natureElezarPrefab;

    public Transform spawnPoint;
    public int enemiesPerWave = 5;
    public Button startWaveButton;
    public Text waveText;

    private int currentWave = 0;
    private int enemiesAlive = 0;
    private bool spawningWave = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        startWaveButton.onClick.AddListener(OnStartWaveButtonPressed);
        UpdateWaveText();
    }

    void OnStartWaveButtonPressed()
    {
        if (!spawningWave && enemiesAlive == 0 && currentWave < 10)
        {
            StartCoroutine(SpawnWave());
        }
    }

    IEnumerator SpawnWave()
    {
        spawningWave = true;
        currentWave++;
        UpdateWaveText();

        int totalEnemiesThisWave = enemiesPerWave + currentWave;

        for (int i = 0; i < totalEnemiesThisWave; i++)
        {
            SpawnProceduralEnemy();
            yield return new WaitForSeconds(1f);
        }

        spawningWave = false;
    }

    void SpawnProceduralEnemy()
    {
        GameObject enemyPrefab = neutralElezarPrefab;
        ElementType element = GetElementForWave();

        switch (element)
        {
            case ElementType.Fire: enemyPrefab = fireElezarPrefab; break;
            case ElementType.Water: enemyPrefab = waterElezarPrefab; break;
            case ElementType.Air: enemyPrefab = airElezarPrefab; break;
            case ElementType.Nature: enemyPrefab = natureElezarPrefab; break;
        }

        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        EnemyStats stats = enemy.GetComponent<EnemyStats>();

        float health = Random.Range(50 + currentWave * 5, 100 + currentWave * 10);
        float speed = Random.Range(1f, 2f + currentWave * 0.1f);
        int gold = Random.Range(5, 10 + currentWave);

        stats.maxHealth = health;
        stats.moveSpeed = speed;
        stats.goldReward = gold;
        stats.elementType = element;

        enemiesAlive++;
    }

    public void EnemyDefeated()
    {
        enemiesAlive--;

        if (enemiesAlive <= 0 && !spawningWave)
        {
            if (currentWave >= 10)
            {
                Debug.Log("🎉 All enemies defeated — YOU WIN!");
                if (WinScreenUI.Instance != null)
                {
                    WinScreenUI.Instance.ShowWinScreen();
                }
                else
                {
                    Debug.LogError("WinScreenUI.Instance is null!");
                }
                startWaveButton.interactable = false;
            }
            else
            {
                startWaveButton.interactable = true;
            }
        }
    }


    void UpdateWaveText()
    {
        if (waveText != null)
            waveText.text = "Wave: " + currentWave + " / 10";
    }

    ElementType GetElementForWave()
    {
        if (currentWave < 3) return ElementType.Neutral;

        int chance = Random.Range(0, 100);

        if (chance < 25) return ElementType.Fire;
        else if (chance < 50) return ElementType.Water;
        else if (chance < 75) return ElementType.Air;
        else return ElementType.Nature;
    }
}
