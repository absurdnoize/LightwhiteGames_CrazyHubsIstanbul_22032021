using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveSound : MonoBehaviour
{
    [SerializeField] private AudioSource au;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag("wave"))
        {
            au.pitch = Random.Range(0.9f, 1.1f);
            au.Play();
        }
    }
}