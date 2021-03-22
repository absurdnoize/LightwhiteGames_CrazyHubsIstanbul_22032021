using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textcolour : MonoBehaviour
{
    private void Start()
    {
        transform.GetComponent<TextMesh>().color = Color.black;
    }
}