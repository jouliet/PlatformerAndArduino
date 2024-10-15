using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinText;
    float coinScore;

    public void AddCoin() {
        coinScore++;
        coinText.text = coinScore.ToString();
    }
}
