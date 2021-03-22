using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrel : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;

    public bool back;
    public float end;
    public Vector3 center;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        switch (Random.Range(0, 3))
        {
            case 0:
                direction = Vector3.right;
                break;

            case 1:
                direction = Vector3.left;
                break;

            case 2:
                if (!back)
                {
                    direction = Vector3.forward;
                }
                else
                {
                    direction = Vector3.back;
                }
                break;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (!back)
        {
            if (transform.position.z > end)
            {
                transform.position = center;
            }
        }
        else
        {
            if (transform.position.z < end)
            {
                transform.position = center;
            }
        }
    }

    private Vector3 direction;

    private void FixedUpdate()
    {
        rb.AddForce(direction * Time.deltaTime * 100);
        if (rb.velocity.magnitude < 10)
        {
            if (!back)
            {
                rb.AddForce(Vector3.forward * Time.deltaTime * 1000);
            }
            else
            {
                rb.AddForce(Vector3.back * Time.deltaTime * 1000);
            }
        }
    }
}