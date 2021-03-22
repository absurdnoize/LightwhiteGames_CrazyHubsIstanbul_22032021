using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bot : MonoBehaviour
{
    // Start is called before the first frame update
    public float bad;

    private void OnTriggerEnter(Collider other)
    {
        if (phase1)
        {
            if (other.CompareTag("wave"))
            {
                theWave = other.transform;
                cc.enabled = false;
                if (Random.Range(0, 100) < bad)
                {
                    StartCoroutine(Jump(1));
                    hooked = true;
                }
                else
                {
                    StartCoroutine(Jump(0));
                }
            }
            if (other.CompareTag("collectable1"))
            {
                ok = true;
                haveTarget = false;
                target = car;
            }
        }
    }

    public void EndOfPhase1()
    {
        foreach (GameObject f in f1)
        {
            f.SetActive(true);
        }
        foreach (GameObject f in f2)
        {
            f.SetActive(false);
        }
        sc.enabled = false;
        ani.applyRootMotion = false;
        ani.SetTrigger("Sit");
        StartCoroutine(Phase2());
        cc.enabled = false;
    }

    [SerializeField] private Transform loc;
    [SerializeField] private GameObject bag;
    [SerializeField] public GameObject[] f1;
    [SerializeField] public GameObject[] f2;
    public int sizeOfLoc = 0;
    private float sizeOfLoc2 = 0;
    public int t1Devidedto = 0;
    private Animator locAni;
    private bool phase1 = true;
    private bool phase2 = false;
    private bool phase3 = false;
    [SerializeField] public SphereCollider sc;

    private IEnumerator Phase2()
    {
        foreach (wave c in FindObjectsOfType<wave>())
        {
            c.hooked = false;
        }
        GetComponent<CapsuleCollider>().enabled = false;
        transform.position = LerpMovement(0);
        transform.LookAt(LerpMovement(0.1f));
        loc.gameObject.SetActive(true);
        phase1 = false;
        loc.GetComponent<locomotif>().bott = this;
        //camera.SetActive(false);
        loc.position = LerpMovement(0.001f) + new Vector3(0, 0.2f, 0);
        foreach (Collectable1 c in GetComponentsInChildren<Collectable1>())
        {
            c.target3 = loc.transform.position;
            c.Phase2();
            sizeOfLoc += 10;

            while (loc.localScale.x < sizeOfLoc)
            {
                loc.localScale = new Vector3(loc.localScale.x + Time.deltaTime * 50, loc.localScale.y + Time.deltaTime * 50, loc.localScale.y + Time.deltaTime * 50);
                yield return new WaitForEndOfFrame();
            }
        }
        t1Devidedto = 2000 / sizeOfLoc;
        sizeOfLoc2 = sizeOfLoc / 10;
        if (t1Devidedto < 10) t1Devidedto = 10;
        bag.SetActive(false);
        locAni = loc.GetComponent<Animator>();
        locAni.SetTrigger("Run");
        locAni.speed = 1;
        //GetComponent<Rigidbody>().isKinematic = false;
        //GetComponent<BoxCollider>().enabled = true;
        //GetComponent<HingeJoint>().connectedBody = loc.GetComponent<Rigidbody>();
        phase2 = true;
    }

    private Vector3 LerpMovement(float t)
    {
        Vector3 ab = new Vector3(transform.position.x, Mathf.Lerp(a.y, b.y, t), Mathf.Lerp(a.z, b.z, t));
        Vector3 bc = new Vector3(transform.position.x, Mathf.Lerp(b.y, c.y, t), Mathf.Lerp(b.z, c.z, t));
        Vector3 cd = new Vector3(transform.position.x, Mathf.Lerp(c.y, d.y, t), Mathf.Lerp(c.z, d.z, t));
        Vector3 abbc = new Vector3(transform.position.x, Mathf.Lerp(ab.y, bc.y, t), Mathf.Lerp(ab.z, bc.z, t));
        Vector3 bccd = new Vector3(transform.position.x, Mathf.Lerp(bc.y, cd.y, t), Mathf.Lerp(bc.z, cd.z, t));
        Vector3 abbcbccd = new Vector3(transform.position.x, Mathf.Lerp(abbc.y, bccd.y, t) - 3.3f, Mathf.Lerp(abbc.z, bccd.z, t));
        return abbcbccd;
    }

    private Transform theWave;
    public Transform target;
    public Transform targetForCollectables;
    public Transform targetForCollectables2;
    [SerializeField] private Transform car;
    [SerializeField] private float speed;
    private CapsuleCollider cc;
    private bool hooked = false;
    public bool ok = true;
    public bool haveTarget = false;

    private IEnumerator Jump(float t)
    {
        yield return new WaitForSeconds(t);
        hooked = false;
        ani.SetTrigger("Jump");
        yield return new WaitForSeconds(Random.Range(0, 1f));
        cc.enabled = true;
    }

    public Animator ani;
    public float aniX = 1;

    [SerializeField]
    private float bFloat;

    [SerializeField]
    private float z;

    [SerializeField]
    private float chunkLenght;

    private Vector3 a;
    private Vector3 b;
    private Vector3 c;
    private Vector3 d;
    [SerializeField] public Track bridge;

    private void Start()
    {
        ani = GetComponent<Animator>();
        cc = GetComponent<CapsuleCollider>();
        ani.SetFloat("Movement", 1);
        bFloat = bridge.chunkLenght;

        sc.radius = Random.Range(0.5f, 1.2f);
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(2);
        chunkLenght = bridge.chunkLenght;
        z = bridge.zz;
        a = bridge.BezierHandles[0];
        b = bridge.BezierHandles[1];
        c = bridge.BezierHandles[2];
        d = bridge.BezierHandles[3];
    }

    // Update is called once per frame
    private void Update()
    {
        if (phase1)
        {
            BotMovement1();
        }
        if (phase2)
        {
            BotMovement2();
        }
        if (phase3)
        {
            BotMovement3();
        }
    }

    private void BotMovement1()
    {
        if (ok)
        {
            //transform.LookAt(new Vector3(car.position.x, transform.position.y, car.position.z));
            //transform.Translate(Vector3.forward * Time.deltaTime * speed);
            Vector3 targetDirection = new Vector3(car.position.x, transform.position.y, car.position.z) - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, speed / 80, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
            ani.speed = aniX;
        }
        else
        {
            if (haveTarget)
            {
                Vector3 targetDirection = new Vector3(target.position.x, transform.position.y, target.position.z) - transform.position;
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, speed / 80, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDirection);
                //transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
                //transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }
        }
        if (hooked)
        {
            if (transform.position.z < 145.9f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, theWave.transform.position.z);
            }
        }
    }

    private float t1 = 0;

    private void BotMovement2()
    {
        locAni.SetFloat("RunBlend", t1);
        locAni.speed = 2f - t1;
        if (t1 < 1)
        {
            t1 += Time.deltaTime / t1Devidedto;
            t1 += (Time.deltaTime / 20) * t1;
            transform.position = LerpMovement(t1);
        }
        else
        {
            EndOfPhase2();
        }
        var targetRotation = Quaternion.LookRotation(LerpMovement(t1 * 1.01f) - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10 * Time.deltaTime);

        if (turn)
        {
            if (canLeft)
            {
                if (left)
                {
                    loc.transform.position = new Vector3(loc.transform.position.x + Time.deltaTime * sizeOfLoc2, loc.transform.position.y, loc.transform.position.z);
                    transform.position = new Vector3(transform.position.x + Time.deltaTime * sizeOfLoc2, transform.position.y, transform.position.z);
                    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y - Time.deltaTime * sizeOfLoc, transform.localEulerAngles.z);
                }
            }
            if (canRight)
            {
                if (right)
                {
                    loc.transform.position = new Vector3(loc.transform.position.x - Time.deltaTime * sizeOfLoc2, loc.transform.position.y, loc.transform.position.z);
                    transform.position = new Vector3(transform.position.x - Time.deltaTime * sizeOfLoc2, transform.position.y, transform.position.z);
                    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + Time.deltaTime * sizeOfLoc, transform.localEulerAngles.z);
                }
            }
        }
        if (transform.position.x < -0.402f)
        {
            canLeft = true;
        }
        else
        {
            canLeft = false;
        }
        if (transform.position.x > -9)
        {
            canRight = true;
        }
        else
        {
            canRight = false;
        }
    }

    public bool turn = false;
    public bool left = false;
    public bool canLeft = false;
    public bool canRight = false;
    public bool right = false;

    private void EndOfPhase2()
    {
        phase2 = false;
        phase3 = true;
        initialPos = transform.position;
        endPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - sizeOfLoc * 0.7f - UnityEngine.Random.Range(-5, 5));
        foreach (GameObject f in f1)
        {
            f.SetActive(false);
        }
        ani.SetTrigger("Lay");
    }

    private Vector3 endPos, initialPos;
    private float t3 = 0;

    private void BotMovement3()
    {
        t3 += Time.deltaTime / (Mathf.Pow(1 + t3, 1 + t3));
        transform.position = new Vector3(transform.position.x, -1.553f, Mathf.Lerp(initialPos.z, endPos.z, t3));
        if (t3 >= 1)
        {
            phase3 = false;
            ani.applyRootMotion = true;
            ani.SetTrigger("Dance");
        }
    }

    private void LateUpdate()
    {
        if (phase1)
            transform.position = new Vector3(transform.position.x, -2.859995f, transform.position.z);
        if (phase2)
        {
            t2 += Time.deltaTime / 50000;
            loc.position = LerpLoc(t1 + t2 + 0.001f);
        }
    }

    private Vector3 LerpLoc(float t)
    {
        Vector3 ab = new Vector3(0, Mathf.Lerp(a.y, b.y, t), Mathf.Lerp(a.z, b.z, t));
        Vector3 bc = new Vector3(0, Mathf.Lerp(b.y, c.y, t), Mathf.Lerp(b.z, c.z, t));
        Vector3 cd = new Vector3(0, Mathf.Lerp(c.y, d.y, t), Mathf.Lerp(c.z, d.z, t));
        Vector3 abbc = new Vector3(0, Mathf.Lerp(ab.y, bc.y, t), Mathf.Lerp(ab.z, bc.z, t));
        Vector3 bccd = new Vector3(0, Mathf.Lerp(bc.y, cd.y, t), Mathf.Lerp(bc.z, cd.z, t));
        Vector3 abbcbccd = new Vector3(loc.transform.position.x, Mathf.Lerp(abbc.y, bccd.y, t) - 3.3f, Mathf.Lerp(abbc.z, bccd.z, t));
        return abbcbccd;
    }

    private float t2 = 0;
}