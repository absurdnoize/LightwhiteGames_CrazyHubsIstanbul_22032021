using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this is don't destroy on load component that carries data from setting menu
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