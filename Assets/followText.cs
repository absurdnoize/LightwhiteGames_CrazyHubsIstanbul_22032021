using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followText : MonoBehaviour
{
    [SerializeField] private Transform tr;
    [SerializeField] private float t1;
    [SerializeField] private float t2;

    // Start is called before the first frame update
    private void Start()
    {
        c = GameObject.FindGameObjectWithTag("c").transform;
        transform.GetComponent<TextMesh>().color = Color.black;
    }

    private Transform c;

    // Update is called once per frame
    private void Update()
    {
        transform.position = tr.position + Vector3.up * 2;
        float t = (transform.position - c.position).magnitude / 30;
        transform.localScale = new Vector3(t, t, t);
    }
}