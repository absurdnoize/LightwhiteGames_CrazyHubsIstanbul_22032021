using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wave : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] [Range(1, 20)] public float speedModifier;

    private void Start()
    {
        t = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        a = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        speed = Random.Range(speedModifier, speedModifier * 2);
    }

    [SerializeField] private Transform t;
    [SerializeField] private Animator a;

    [SerializeField] private float speed;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (collision.GetComponent<Player>().phase1)
            {
                a.applyRootMotion = false;
                hooked = true;
            }
        }
        if (collision.transform.CompareTag("collectable1"))
        {
            collision.transform.position = new Vector3(Random.Range(7, -17), collision.transform.position.y, Random.Range(3, 150));
        }
        if (collision.transform.CompareTag("diamond"))
        {
            collision.transform.position = new Vector3(Random.Range(7, -17), collision.transform.position.y, Random.Range(3, 150));
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            a.applyRootMotion = true;
            hooked = false;
        }
    }

    public bool hooked = false;

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        if (hooked)
        {
            if (Input.GetButtonUp("Fire1"))
            {
                Vector3 mousePos = Input.mousePosition;
                {
                    if (mousePos.y > Screen.height / 2)
                    {
                        hooked = false;
                        a.applyRootMotion = true;
                    }
                }
            }

            t.transform.position = new Vector3(t.transform.position.x, t.transform.position.y, t.transform.position.z + (Time.deltaTime * speed));
        }
        if (transform.position.z > 148)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -50);
        }
    }
}