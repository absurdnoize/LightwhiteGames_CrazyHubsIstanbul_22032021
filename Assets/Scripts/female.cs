using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// not used
public class female : MonoBehaviour
{
    private Transform target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.LookAt(target);
    }
}