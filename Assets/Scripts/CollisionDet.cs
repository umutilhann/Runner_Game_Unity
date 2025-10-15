using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollisionDet : MonoBehaviour
{
    [SerializeField] GameObject thePlayer;
    [SerializeField] GameObject playerAnimation;
    void OnTriggerEnter(Collider other)
    {
        thePlayer.GetComponent<PlayerMovement>().enabled = false;
        playerAnimation.GetComponent<Animator>().Play("Stumble Backwards");
    }
}
