using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2trigger : MonoBehaviour
{
    public bool forBot = false;

    private void OnTriggerEnter(Collider other)
    {
        if (forBot)
        {
            if (other.CompareTag("Bot"))
            {
                other.gameObject.GetComponent<bot>().EndOfPhase1();
            }
        }
        else
        {
            if (other.CompareTag("Player"))
            {
                other.gameObject.GetComponent<Player>().EndOfPhase1();
            }
        }
    }
}