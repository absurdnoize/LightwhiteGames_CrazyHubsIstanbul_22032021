using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// bot eyes whem they drive
public class boteye : MonoBehaviour
{
    [SerializeField] private bool middle = false;
    [SerializeField] private bool left = false;
    [SerializeField] private bool right = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("barrel"))
        {
            if (middle)
                bott.turn = true;
            if (left)
                bott.left = false;
            if (right)
                bott.right = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("barrel"))
        {
            if (middle)
                bott.turn = false;
            if (left)
                bott.left = true;
            if (right)
                bott.right = true;
        }
    }

    private bot bott;

    private void Start()
    {
        bott = GetComponentInParent<bot>();
    }

    // Update is called once per frame
    private void Update()
    {
    }
}