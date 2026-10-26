using TMPro;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.InputSystem.Controls;

public class CurrencySystem : MonoBehaviour
{
    public TextMeshProUGUI currency_text;
    private float currency_timer;
    
    // Adds the correct amount of currency to coins and total coins based on the coin value upgrade
    public void GainCurrency()
    {
        PlayerData.coins += PlayerData.upgrade_dict["coin_value"] + 5;
        PlayerData.total_coins_alltime += PlayerData.upgrade_dict["coin_value"] + 5;
    }

    // Deducts the correct amount of currency based on the error deduction upgrade
    public void LoseCurrency()
    {
        PlayerData.coins -= 3 - PlayerData.upgrade_dict["error_deduction"];
    }

    // Updates the coin currency text
    private void UpdateText()
    {
        currency_text.text = $"Coins: {PlayerData.coins}";
    }

    // Generates the correct number of coins based on the coin generator upgrade
    private void GenerateCurrency()
    {
        if(PlayerData.upgrade_dict["coin_generator"] > 0)
        {
            PlayerData.coins += PlayerData.upgrade_dict["coin_generator"];
            PlayerData.total_coins_alltime += PlayerData.upgrade_dict["coin_generator"];
        }
    }

    void Update()
    {
        UpdateText();

    // Limits currency from going below 0
        if(PlayerData.coins < 0)
        {
            PlayerData.coins = 0;
        }

    // Generates passive currency every 5 seconds for the coin generator upgrade
        currency_timer += Time.deltaTime;
        if(currency_timer > 5f)
        {
            GenerateCurrency();
            currency_timer = 0;
        }
    }
}
