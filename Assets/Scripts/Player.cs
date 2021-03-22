using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Utilities;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private AudioClip[] auCl;
    [SerializeField] private AudioSource au;
    [SerializeField] public Animator animator;
    [SerializeField] public Transform targetForCollectables;
    [SerializeField] public Transform targetForCollectables2;
    [SerializeField] public GameObject camera;
    [SerializeField] public GameObject next;
    [SerializeField] public GameObject bag;
    [SerializeField] public GameObject fail;
    [SerializeField] public GameObject diamondObj;
    [SerializeField] public Transform loc;
    [SerializeField] public GameObject[] f1;
    [SerializeField] public GameObject[] f2;
    [SerializeField] public Transform[] f3;
    [SerializeField] public Transform[] f4;
    [SerializeField] public Track bridge;
    [SerializeField] private TMPro.TextMeshProUGUI diamondText;
    [SerializeField] private TMPro.TextMeshProUGUI posText;
    [SerializeField] private GameObject posTextObj;

    public int diamond = 10;
    private float _animatorControl = 0;
    private bool movementStop = false;

    public float swipeThreshold = 40f;
    public float timeThreshold = 0.3f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("diamond"))
        {
            other.GetComponent<diamond>().Collected();
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("Gameplay");
    }

    private void Fail()
    {
        fail.SetActive(true);
        Time.timeScale = 0.05f;
    }

    private void Fail2()
    {
        fail.SetActive(true);
        Time.timeScale = 0f;
    }

    public void AddDiamond()
    {
        diamond++;
        diamondText.SetText(diamond.ToString());
        au.clip = auCl[0];
        au.Play();
    }

    public IEnumerator AddDiamondC(int z)
    {
        int k = 0;
        next.SetActive(true);
        while (k < z)
        {
            k++;
            diamond++;
            diamondText.SetText(diamond.ToString());
            yield return new WaitForSeconds(0.05f);
        }
    }

    [SerializeField] private GameObject devPanel;

    public void Dev()
    {
        devPanel.SetActive(true);
        cc.enabled = false;
        animator.applyRootMotion = false;
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        Time.timeScale = 1f;
        diamondText.SetText(diamond.ToString());
        cc = GetComponent<CharacterController>();

        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(2);
        bFloat = bridge.chunkLenght;
        z = bridge.zz;
        a = bridge.BezierHandles[0];
        b = bridge.BezierHandles[1];
        c = bridge.BezierHandles[2];
        d = bridge.BezierHandles[3];
    }

    // Update is called once per frame
    private float t1 = 0;

    public bool phase1 = true;
    public bool phase2 = false;
    public bool phase3 = false;

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
        animator.applyRootMotion = false;
        animator.SetTrigger("Sit");
        StartCoroutine(Phase2());
    }

    private int pos = 1;

    public void EndOfPhase2()
    {
        phase2 = false;
        phase3 = true;
        initialPos = transform.position;
        endPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - sizeOfLoc * 0.7f - UnityEngine.Random.Range(-5, 5));
        foreach (Transform f in f4)
        {
            if (f.position.z < transform.position.z)
            {
                pos++;
            }
        }
        posText.SetText(pos.ToString());
        posTextObj.SetActive(true);
        if (pos == 1)
        {
            winner = true;
        }
        foreach (GameObject f in f1)
        {
            f.SetActive(false);
        }
        animator.SetTrigger("Lay");
        foreach (Transform tr in f3)
        {
            tr.position = new Vector3(transform.position.x + 1, tr.position.y, tr.position.z);
        }
    }

    private bool winner = false;
    public float sizeOfLocX;

    private IEnumerator Phase2()
    {
        foreach (wave c in FindObjectsOfType<wave>())
        {
            c.hooked = false;
        }

        GetComponent<CharacterController>().enabled = false;
        transform.position = LerpMovement(0);
        transform.LookAt(LerpMovement(0.1f));
        loc.gameObject.SetActive(true);
        phase1 = false;
        //female fe = FindObjectOfType<female>();
        //fe.transform.position = new Vector3(transform.position.x - 0.6f, -3.3f, -0.117f);
        //fe.transform.LookAt(LerpMovement(0.1f));
        //fe.GetComponent<Animator>().SetTrigger("Sit");
        //fe.GetComponent<Animator>().applyRootMotion = false;
        //fe.transform.SetParent(this.transform);
        //fe.enabled = false;
        camera.GetComponent<CameraFollow>().Phase2();
        //camera.SetActive(false);
        loc.position = LerpMovement(0.001f) + new Vector3(0, 0.2f, 0);
        foreach (Collectable1 c in GetComponentsInChildren<Collectable1>())
        {
            c.target3 = loc.transform.position;
            c.Phase2();
            sizeOfLoc += sizeOfLocX;

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
        au.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
        au.clip = auCl[1];
        au.Play();
        //GetComponent<Rigidbody>().isKinematic = false;
        //GetComponent<BoxCollider>().enabled = true;
        //GetComponent<HingeJoint>().connectedBody = loc.GetComponent<Rigidbody>();
        phase2 = true;
    }

    public void Loc()
    {
        au.clip = auCl[2];
        au.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
        au.Play();
        sizeOfLoc -= 10;
    }

    private Animator locAni;
    public float sizeOfLoc = 0;
    private float sizeOfLoc2 = 0;

    private void Update()
    {
        if (phase1)
        {
            PlayerMovement1();
        }
        if (phase2)
        {
            PlayerMovement2();
        }
        if (phase3)
        {
            PlayerMovement3();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            right = false;
            left = false;
            Vector3 mousePos = Input.mousePosition;
            {
                if (mousePos.y > Screen.height / 2)
                {
                    if (lock1)
                    {
                        lock1 = false;
                        animator.SetTrigger("Running Jump");
                        StartCoroutine(Lock1());
                    }
                }
            }
        }
        if (Input.GetButton("Fire1"))
        {
            Vector3 mousePos = Input.mousePosition;
            {
                if (mousePos.x < Screen.width / 2)
                {
                    right = true;
                    left = false;
                }
                else
                {
                    right = false;
                    left = true;
                }
            }
        }

        if (left)
        {
            if (phase2)
            {
                if (transform.position.x > -9)
                {
                    loc.transform.position = new Vector3(loc.transform.position.x - Time.deltaTime * sizeOfLoc2, loc.transform.position.y, loc.transform.position.z);
                    transform.position = new Vector3(transform.position.x - Time.deltaTime * sizeOfLoc2, transform.position.y, transform.position.z);
                    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + Time.deltaTime * sizeOfLoc, transform.localEulerAngles.z);
                }
            }
            if (phase1)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + Time.deltaTime * 200, transform.localEulerAngles.z);
            }
        }
        if (right)
        {
            if (phase2)
            {
                if (transform.position.x < -0.402f)
                {
                    loc.transform.position = new Vector3(loc.transform.position.x + Time.deltaTime * sizeOfLoc2, loc.transform.position.y, loc.transform.position.z);
                    transform.position = new Vector3(transform.position.x + Time.deltaTime * sizeOfLoc2, transform.position.y, transform.position.z);
                    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y - Time.deltaTime * sizeOfLoc, transform.localEulerAngles.z);
                }
            }
            if (phase1)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y - Time.deltaTime * 200, transform.localEulerAngles.z);
            }
        }
    }

    private Text txt;

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

    private bool left = false;

    public void Left()
    {
        left = true;
    }

    private bool right = false;

    public void Right()
    {
        right = true;
    }

    private void PlayerMovement1()
    {
        if (transform.position.z > 146.5f || transform.position.x < -19 || transform.position.x > 8.5f)
        {
            Fail();
            if (transform.position.z > 148f)
                Fail2();
        }
        animator.SetFloat("Movement", 1);
        if (Input.GetKey("a"))
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y - Time.deltaTime * 200, transform.localEulerAngles.z);
        }
        if (Input.GetKey("d"))
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + Time.deltaTime * 200, transform.localEulerAngles.z);
        }
        if (Input.GetKey("space"))
        {
            if (lock1)
            {
                lock1 = false;
                animator.SetTrigger("Running Jump");
                StartCoroutine(Lock1());
            }
        }
    }

    private CharacterController cc;

    private IEnumerator Lock1()
    {
        cc.enabled = false;
        //Vector3 initial = transform.position;
        float t = 0;
        cc.center = new Vector3(0, 0.4f, 0);
        yield return new WaitForSeconds(1);
        //transform.position = initial;

        cc.enabled = true;
        lock1 = true;
    }

    private bool lock1 = true;
    public float t1Devidedto;

    private void PlayerMovement2()
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

        if (Input.GetKey("a"))
        {
            if (transform.position.x < -0.402f)
            {
                loc.transform.position = new Vector3(loc.transform.position.x + Time.deltaTime * sizeOfLoc2, loc.transform.position.y, loc.transform.position.z);
                transform.position = new Vector3(transform.position.x + Time.deltaTime * sizeOfLoc2, transform.position.y, transform.position.z);
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y - Time.deltaTime * sizeOfLoc, transform.localEulerAngles.z);
            }
        }
        if (Input.GetKey("d"))
        {
            if (transform.position.x > -9)
            {
                loc.transform.position = new Vector3(loc.transform.position.x - Time.deltaTime * sizeOfLoc2, loc.transform.position.y, loc.transform.position.z);
                transform.position = new Vector3(transform.position.x - Time.deltaTime * sizeOfLoc2, transform.position.y, transform.position.z);
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + Time.deltaTime * sizeOfLoc, transform.localEulerAngles.z);
            }
        }
    }

    private Vector3 endPos, initialPos;
    private float t3 = 0;

    private void PlayerMovement3()
    {
        t3 += Time.deltaTime / (Mathf.Pow(1 + t3, 1 + t3));
        transform.position = new Vector3(transform.position.x, -1.553f, Mathf.Lerp(initialPos.z, endPos.z, t3));
        if (t3 >= 1)
        {
            phase3 = false;
            camera.GetComponent<CameraFollow>().Phase3();
            animator.applyRootMotion = true;
            if (winner)
                animator.SetTrigger("Dance");
            else
                animator.SetTrigger("Cry");
            StartCoroutine(Diamonds());
        }
    }

    private IEnumerator Diamonds()
    {
        int n = 17;
        int j = 0;
        int l = 0;
        int o = 0;

        for (int i = 0; i < n; i++)
        {
            if (transform.position.z < bFloat - 23.5f - j)
            {
                l = i;
                n = 17;
            }
            j += 5;
        }
        Debug.Log(chunkLenght);
        switch (l)
        {
            case 0:
                o = 1;
                break;

            case 1:
                o = 2;
                break;

            case 2:
                o = 3;
                break;

            case 3:
                o = 4;
                break;

            case 4:
                o = 5;
                break;

            case 5:
                o = 6;
                break;

            case 6:
                o = 7;
                break;

            case 7:
                o = 8;
                break;

            case 9:
                o = 10;
                break;

            case 10:
                o = 15;
                break;

            case 11:
                o = 20;
                break;

            case 12:
                o = 25;
                break;

            case 13:
                o = 50;
                break;

            case 14:
                o = 100;
                break;

            case 15:
                o = 200;
                break;

            case 16:
                o = 500;
                break;
        }
        posText.SetText((4 - pos).ToString() + "x" + o.ToString());
        StartCoroutine(AddDiamondC((4 - pos) * o));
        yield return new WaitForSeconds(0.05f);
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

    private float t2 = 0;

    // If the touch is longer than MAX_SWIPE_TIME, we dont consider it a swipe
    public const float MAX_SWIPE_TIME = 60f;

    // Factor of the screen width that we consider a swipe
    // 0.17 works well for portrait mode 16:9 phone
    public const float MIN_SWIPE_DISTANCE = 0.1f;

    public static bool swipedRight = false;
    public static bool swipedLeft = false;
    public static bool swipedUp = false;
    public static bool swipedDown = false;

    public bool debugWithArrowKeys = true;

    private Vector2 startPos;
    private float startTime;
}