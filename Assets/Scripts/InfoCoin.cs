using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InfoCoin : MonoBehaviour
{
    public static int totalCoins = 0;
    [SerializeField] GameObject coinDisplay;
    

    // Update is called once per frame
    void Update()
    {
        coinDisplay.GetComponent<TMPro.TMP_Text>().text = "ALTIN: " + totalCoins;
    }
}
