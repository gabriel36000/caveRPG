using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinTextManager : MonoBehaviour
{
    public inventory playerInventory;
    public TextMeshProUGUI coinDisplay;

    public void UpdateCoinCount() {
        coinDisplay.text = "" + playerInventory.coins;
    }
   

}
