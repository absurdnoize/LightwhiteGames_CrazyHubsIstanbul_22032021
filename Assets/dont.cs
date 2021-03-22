using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dont : MonoBehaviour
{
    [SerializeField] public float[] savedValue;
    public bool load;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("load");

        if (objs.Length > 1)
        {
            Destroy(objs[1]);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}