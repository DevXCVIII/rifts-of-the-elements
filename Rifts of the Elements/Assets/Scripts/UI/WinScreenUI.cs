using UnityEngine;

public class WinScreenUI : MonoBehaviour
{
    public GameObject winScreen;

    public static WinScreenUI Instance;

    void Awake()
    {
        Instance = this;
        winScreen.SetActive(false);
    }

    public void ShowWinScreen()
    {
        if (winScreen == null)
        {
            Debug.LogError("WinScreen is not assigned in the Inspector!");
            return;
        }

        Debug.Log("Displaying Win Screen.");
        winScreen.SetActive(true);
    }
}
