using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollectCoin : MonoBehaviour
{
    [SerializeField] AudioSource coinFX;
    void OnTriggerEnter(Collider other)
    {
        coinFX.Play();
        InfoCoin.totalCoins += 1;
        this.gameObject.SetActive(false);
    }
}
