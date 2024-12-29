using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinText;
    float coinScore;
    [SerializeField] TextMeshProUGUI healthText;

    public void AddCoin() {
        coinScore++;
        coinText.text = coinScore.ToString();
    }
    public void UpdateHealth(float health)
    {
        healthText.text = health.ToString();
    }
}
