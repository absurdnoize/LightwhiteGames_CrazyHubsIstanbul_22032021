using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botTarger : MonoBehaviour
{
    private void Start()
    {
        bot = GetComponentInParent<bot>();
    }

    [SerializeField]
    private bot bot;

    private Transform collectable;
    private Vector3 initial;
    private bool contact = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("collectable1"))
        {
            collectable = other.transform;
            initial = other.transform.position;
            bot.target = other.transform;
            contact = true;
        }
        if (other.CompareTag("wave"))
        {
            contact = false;
        }
    }

    private void Update()
    {
        if (contact)
        {
            if (initial == collectable.position)
            {
                bot.ok = false;
                bot.haveTarget = true;
            }
            else
            {
                bot.ok = true;
            }
        }
        else
        {
            bot.ok = true;
        }
    }
}