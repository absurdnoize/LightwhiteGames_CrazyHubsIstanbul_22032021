using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable1 : MonoBehaviour
{
    private Player player;
    private AudioSource au;
    private bool collected = false;
    public Vector3 target1, target2, target3;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            au.pitch = Random.Range(0.9f, 1.1f);
            au.Play();
            t = 0;
            rb.isKinematic = false;
            target3 = player.targetForCollectables.position;
            target2 = player.targetForCollectables2.position;
            target1 = this.transform.position;
            collected = true;
            tr.enabled = true;
            bc.enabled = false;
            cc.enabled = true;
            //this.GetComponent<Animator>().applyRootMotion = false;
            //this.GetComponent<Animator>().StopPlayback();
            transform.tag = "Untagged";
        }

        if (collision.transform.CompareTag("Bot"))
        {
            t = 0;
            rb.isKinematic = false;
            bott = collision.GetComponent<bot>();
            collision.GetComponent<bot>().haveTarget = false;
            target1 = this.transform.position;
            collected = true;
            tr.enabled = true;
            bc.enabled = false;
            cc.enabled = true;
            transform.tag = "Untagged";
        }
    }

    private bot bott;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            transform.parent = null;
            cc.isTrigger = true;
            rb.useGravity = false;
            rb.isKinematic = true;
            transform.localEulerAngles = Vector3.zero;
            transform.position = new Vector3(transform.position.x, -2.9f, transform.position.z);
            bc.enabled = true;
            cc.enabled = false;
            //this.GetComponent<Animator>().applyRootMotion = true;
            //this.GetComponent<Animator>().StartPlayback();
            transform.localEulerAngles = new Vector3(0, 0, 0);
            transform.tag = "collectable1";
        }
    }

    private Rigidbody rb;
    private TrailRenderer tr;
    private CapsuleCollider cc;
    private BoxCollider bc;

    public void Phase2()
    {
        t = 0;
        rb.isKinematic = false;
        target2 = player.targetForCollectables2.position;
        target1 = this.transform.position;
        collected = true;
        phase2 = true;
        tr.enabled = true;
        bc.enabled = false;
        cc.enabled = false;
    }

    // Start is called before the first frame update
    private void Start()
    {
        //int z = Random.Range(1, 4);

        //transform.localScale = new Vector3(z, z, z);
        au = GetComponent<AudioSource>();
        transform.localEulerAngles = Vector3.zero;
        transform.position = new Vector3(transform.position.x, -2.6f, transform.position.z);
        transform.position = new Vector3(Random.Range(7, -17), transform.position.y, Random.Range(3, 148));
        rb = this.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        tr = this.GetComponent<TrailRenderer>();
        cc = this.GetComponent<CapsuleCollider>();
        bc = this.GetComponent<BoxCollider>();
    }

    private bool phase2 = false;

    // Update is called once per frame
    private void Update()
    {
        if (collected)
        {
            if (bott == null)
            {
                if (!phase2)
                    target3 = player.targetForCollectables.position;
                target2 = player.targetForCollectables2.position;
                t += Time.deltaTime * 2;
                if (t >= 1)
                {
                    collected = false;
                    this.transform.parent = player.transform;
                    rb.useGravity = true;
                    tr.enabled = false;
                    cc.isTrigger = false;
                    if (phase2)
                    {
                        this.gameObject.SetActive(false);
                    }
                }
                LerpPos();
                transform.position = l3;
            }
            else
            {
                if (!phase2)
                    target3 = bott.targetForCollectables.position;
                target2 = bott.targetForCollectables2.position;
                t += Time.deltaTime * 2;
                if (t >= 1)
                {
                    collected = false;
                    this.transform.parent = bott.transform;
                    rb.useGravity = true;
                    tr.enabled = false;
                    cc.isTrigger = false;
                    if (phase2)
                    {
                        this.gameObject.SetActive(false);
                    }
                }
                LerpPos();
                transform.position = l3;
            }
        }
    }

    private float t = 0;
    private Vector3 l1, l2, l3;

    private void LerpPos()
    {
        l1 = new Vector3(Mathf.Lerp(target1.x, target2.x, t), Mathf.Lerp(target1.y, target2.y, t), Mathf.Lerp(target1.z, target2.z, t));
        l2 = new Vector3(Mathf.Lerp(target2.x, target3.x, t), Mathf.Lerp(target2.y, target3.y, t), Mathf.Lerp(target2.z, target3.z, t));
        l3 = new Vector3(Mathf.Lerp(l1.x, l2.x, t), Mathf.Lerp(l1.y, l2.y, t), Mathf.Lerp(l1.z, l2.z, t));
    }
}