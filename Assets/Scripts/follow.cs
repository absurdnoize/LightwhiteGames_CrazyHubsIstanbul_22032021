using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void LateUpdate()
    {
        transform.position = target.transform.position;
    }
}