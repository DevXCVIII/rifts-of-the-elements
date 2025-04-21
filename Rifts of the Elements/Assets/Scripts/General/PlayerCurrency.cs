using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerCurrency : MonoBehaviour
{
    public static PlayerCurrency Instance;
    public int currentCurrency = 100;  // Start with 100 as an example

    public Text currencyText;              // Reference to the UI Text
    public Text insufficientFundsText;     // Assign this in the inspector

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (currencyText != null)
        {
            currencyText.text = "Currency: " + currentCurrency.ToString();
        }
    }

    public bool CanAfford(int cost)
    {
        return currentCurrency >= cost;
    }

    public void SpendCurrency(int amount)
    {
        if (CanAfford(amount))
        {
            currentCurrency -= amount;
        }
        else
        {
            Debug.LogWarning("Not enough currency to place tower!");
            ShowInsufficientFunds(); // trigger feedback
        }
    }

    public void AddCurrency(int amount)
    {
        currentCurrency += amount;
    }

    public void ShowInsufficientFunds()
    {
        StopAllCoroutines();
        StartCoroutine(FlashInsufficientFunds());
    }

    IEnumerator FlashInsufficientFunds()
    {
        if (insufficientFundsText != null)
        {
            insufficientFundsText.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            insufficientFundsText.gameObject.SetActive(false);
        }
    }
}
