using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class dev : MonoBehaviour
{
    // Start is called before the first frame
    [SerializeField] public Camera c;

    [SerializeField] public wave[] w;
    [SerializeField] public bot[] b;

    [SerializeField] public CameraFollow cf;
    [SerializeField] public Track tr;
    [SerializeField] public Player pl;

    [SerializeField] public Text[] valueShowTextArray;
    [SerializeField] public Slider[] sliderArray;

    [SerializeField] public Button[] buttonArray;
    [SerializeField] public float[] savedValue;
    [SerializeField] public float[] defaultValue;

    private void AddL()
    {
        int i = 0;
        foreach (Slider f in sliderArray)
        {
            switch (i)
            {
                case 0:
                    f.onValueChanged.AddListener(delegate { Listeners0(); });
                    break;

                case 1:
                    f.onValueChanged.AddListener(delegate { Listeners1(); });
                    break;

                case 2:
                    f.onValueChanged.AddListener(delegate { Listeners2(); });
                    break;

                case 3:
                    f.onValueChanged.AddListener(delegate { Listeners3(); });
                    break;

                case 4:
                    f.onValueChanged.AddListener(delegate { Listeners4(); });
                    break;

                case 5:
                    f.onValueChanged.AddListener(delegate { Listeners5(); });
                    break;

                case 6:
                    f.onValueChanged.AddListener(delegate { Listeners6(); });
                    break;

                case 7:
                    f.onValueChanged.AddListener(delegate { Listeners7(); });
                    break;

                case 8:
                    f.onValueChanged.AddListener(delegate { Listeners8(); });
                    break;

                case 9:
                    f.onValueChanged.AddListener(delegate { Listeners9(); });
                    break;

                case 10:
                    f.onValueChanged.AddListener(delegate { Listeners10(); });
                    break;

                case 11:
                    f.onValueChanged.AddListener(delegate { Listeners11(); });
                    break;

                case 12:
                    f.onValueChanged.AddListener(delegate { Listeners12(); });
                    break;

                case 13:
                    f.onValueChanged.AddListener(delegate { Listeners13(); });
                    break;

                case 14:
                    f.onValueChanged.AddListener(delegate { Listeners14(); });
                    break;
            }
            i++;
        }
        i = 0;
        foreach (Button f in buttonArray)
        {
            switch (i)
            {
                case 0:
                    f.onClick.AddListener(delegate { Reset0(); });
                    break;

                case 1:
                    f.onClick.AddListener(delegate { Reset1(); });
                    break;

                case 2:
                    f.onClick.AddListener(delegate { Reset2(); });
                    break;

                case 3:
                    f.onClick.AddListener(delegate { Reset3(); });
                    break;

                case 4:
                    f.onClick.AddListener(delegate { Reset4(); });
                    break;

                case 5:
                    f.onClick.AddListener(delegate { Reset5(); });
                    break;

                case 6:
                    f.onClick.AddListener(delegate { Reset6(); });
                    break;

                case 7:
                    f.onClick.AddListener(delegate { Reset7(); });
                    break;

                case 8:
                    f.onClick.AddListener(delegate { Reset8(); });
                    break;

                case 9:
                    f.onClick.AddListener(delegate { Reset9(); });
                    break;

                case 10:
                    f.onClick.AddListener(delegate { Reset10(); });
                    break;

                case 11:
                    f.onClick.AddListener(delegate { Reset11(); });
                    break;

                case 12:
                    f.onClick.AddListener(delegate { Reset12(); });
                    break;

                case 13:
                    f.onClick.AddListener(delegate { Reset13(); });
                    break;

                case 14:
                    f.onClick.AddListener(delegate { Reset14(); });
                    break;
            }
            i++;
        }
    }

    private void Listeners0()
    {
        float f = Mathf.Lerp(1f, 179f, sliderArray[0].value);
        Debug.Log(f);
        valueShowTextArray[0].text = f.ToString("N0");
        c.fieldOfView = f;
        savedValue[0] = f;
    }

    private void Listeners1()
    {
        float f = Mathf.Lerp(0f, 5f, sliderArray[1].value);
        Debug.Log(f);
        valueShowTextArray[1].text = f.ToString("N0");
        cf.SmoothTime = f;
        savedValue[1] = f;
    }

    private void Listeners2()
    {
        float f = Mathf.Lerp(-5f, 5f, sliderArray[2].value);
        Debug.Log(f);
        valueShowTextArray[2].text = f.ToString("N0");
        cf.LookOffset = f;
        savedValue[2] = f;
    }

    private void Listeners3()
    {
        float f = Mathf.Lerp(-20f, 20f, sliderArray[3].value);
        Debug.Log(f);
        valueShowTextArray[3].text = f.ToString("N0");
        cf.Offset = new Vector3(f, cf.Offset.y, cf.Offset.z);
        savedValue[3] = f;
    }

    private void Listeners4()
    {
        float f = Mathf.Lerp(-20f, 20f, sliderArray[4].value);
        Debug.Log(f);
        valueShowTextArray[4].text = f.ToString("N0");
        cf.Offset = new Vector3(cf.Offset.x, f, cf.Offset.z);
        savedValue[4] = f;
    }

    private void Listeners5()
    {
        float f = Mathf.Lerp(-20f, 20f, sliderArray[5].value);
        Debug.Log(f);
        valueShowTextArray[5].text = f.ToString("N0");
        cf.Offset = new Vector3(cf.Offset.x, cf.Offset.y, f);
        savedValue[5] = f;
    }

    private void Listeners6()
    {
        float f = Mathf.Lerp(1, 20f, sliderArray[6].value);
        Debug.Log(f);
        valueShowTextArray[6].text = f.ToString("N0");
        foreach (wave w in w)
        {
            w.speedModifier = f;
        }
        savedValue[6] = f;
    }

    private void Listeners7()
    {
        float f = Mathf.Lerp(1000f, 5000f, sliderArray[7].value);
        Debug.Log(f);
        valueShowTextArray[7].text = f.ToString("N0");
        tr.chunkLenght = Mathf.RoundToInt(-f);
        savedValue[7] = -f;
    }

    private void Listeners8()
    {
        float f = Mathf.Lerp(1, 10, sliderArray[8].value);
        Debug.Log(f);
        valueShowTextArray[8].text = f.ToString("N0");
        tr.randomizeBridge = f;
        savedValue[8] = f;
    }

    private void Listeners9()
    {
        float f = Mathf.Lerp(0.1f, 2f, sliderArray[9].value);
        Debug.Log(f);
        valueShowTextArray[9].text = f.ToString("N0");
        foreach (bot b in b)
        {
            b.sc.radius = f;
        }
        savedValue[9] = f;
    }

    private void Listeners10()
    {
        float f = Mathf.Lerp(0.1f, 2f, sliderArray[10].value);
        Debug.Log(f);
        valueShowTextArray[10].text = f.ToString("N0");
        foreach (bot b in b)
        {
            b.aniX = f;
        }
        savedValue[10] = f;
    }

    private void Listeners11()
    {
        float f = Mathf.Lerp(0, 100, sliderArray[11].value);
        Debug.Log(f);
        valueShowTextArray[11].text = f.ToString("N0");
        foreach (bot b in b)
        {
            b.bad = f;
        }

        savedValue[11] = f;
    }

    private void Listeners12()
    {
        float f = Mathf.Lerp(1, 100, sliderArray[12].value);
        Debug.Log(f);
        valueShowTextArray[12].text = f.ToString("N0");
        pl.swipeThreshold = f;
        savedValue[12] = f;
    }

    private void Listeners13()
    {
        float f = Mathf.Lerp(0.1f, 2, sliderArray[13].value);
        Debug.Log(f);
        valueShowTextArray[13].text = f.ToString("N0");
        pl.animator.speed = f;
        savedValue[13] = f;
    }

    private void Listeners14()
    {
        float f = Mathf.Lerp(5, 15, sliderArray[14].value);
        Debug.Log(f);
        valueShowTextArray[14].text = f.ToString("N0");
        pl.sizeOfLocX = f;
        savedValue[14] = f;
    }

    private void Reset0()
    {
        sliderArray[0].value = Mathf.InverseLerp(1f, 179f, defaultValue[0]);
        Listeners0();
    }

    private void Reset1()
    {
        sliderArray[1].value = Mathf.InverseLerp(0f, 5f, defaultValue[1]);
        Listeners1();
    }

    private void Reset2()
    {
        sliderArray[2].value = Mathf.InverseLerp(-5f, 5f, defaultValue[2]);
        Listeners2();
    }

    private void Reset3()
    {
        sliderArray[3].value = Mathf.InverseLerp(-20f, 20f, defaultValue[3]);
        Listeners3();
    }

    private void Reset4()
    {
        sliderArray[4].value = Mathf.InverseLerp(-20f, 20f, defaultValue[4]);
        Listeners4();
    }

    private void Reset5()
    {
        sliderArray[5].value = Mathf.InverseLerp(-20f, 20f, defaultValue[5]);
        Listeners5();
    }

    private void Reset6()
    {
        sliderArray[6].value = Mathf.InverseLerp(1f, 20f, defaultValue[6]);
        Listeners6();
    }

    private void Reset7()
    {
        sliderArray[7].value = Mathf.InverseLerp(1000f, 5000f, defaultValue[7]);
        Listeners7();
    }

    private void Reset8()
    {
        sliderArray[8].value = Mathf.InverseLerp(1, 10, defaultValue[8]);
        Listeners8();
    }

    private void Reset9()
    {
        sliderArray[9].value = Mathf.InverseLerp(0.1f, 2f, defaultValue[9]);
        Listeners9();
    }

    private void Reset10()
    {
        sliderArray[10].value = Mathf.InverseLerp(0.1f, 2f, defaultValue[10]);
        Listeners10();
    }

    private void Reset11()
    {
        sliderArray[11].value = Mathf.InverseLerp(0, 100, defaultValue[11]);
        Listeners11();
    }

    private void Reset12()
    {
        sliderArray[12].value = Mathf.InverseLerp(1, 100, defaultValue[12]);
        Listeners12();
    }

    private void Reset13()
    {
        sliderArray[13].value = Mathf.InverseLerp(0.1f, 2f, defaultValue[13]);
        Listeners13();
    }

    private void Reset14()
    {
        sliderArray[14].value = Mathf.InverseLerp(5f, 15f, defaultValue[14]);
        Listeners14();
    }

    public bool load;

    public void Load()
    {
        dontd.load = true;
        dontd.savedValue = savedValue;
        SceneManager.LoadScene("Gameplay");
    }

    public void DontLoad()
    {
        dontd.load = false;
        SceneManager.LoadScene("Gameplay");
    }

    private dont dontd;

    private void Start()
    {
        dontd = GameObject.FindGameObjectWithTag("load").GetComponent<dont>();
        load = dontd.load;
        if (dontd.savedValue.Length > 0)
            savedValue = dontd.savedValue;

        AddL();

        if (load)
        {
            int i = 0;
            foreach (Slider f in sliderArray)
            {
                switch (i)
                {
                    case 0:
                        sliderArray[0].value = Mathf.InverseLerp(1f, 179f, savedValue[0]);
                        Listeners0();
                        break;

                    case 1:
                        sliderArray[1].value = Mathf.InverseLerp(0f, 5f, savedValue[1]);
                        Listeners1();
                        break;

                    case 2:
                        sliderArray[2].value = Mathf.InverseLerp(-5f, 5f, savedValue[2]);
                        Listeners2();
                        break;

                    case 3:
                        sliderArray[3].value = Mathf.InverseLerp(-20f, 20f, savedValue[3]);
                        Listeners3();
                        break;

                    case 4:
                        sliderArray[4].value = Mathf.InverseLerp(-20f, 20f, savedValue[4]);
                        Listeners4();
                        break;

                    case 5:
                        sliderArray[5].value = Mathf.InverseLerp(-20f, 20f, savedValue[5]);
                        Listeners5();
                        break;

                    case 6:
                        sliderArray[6].value = Mathf.InverseLerp(1f, 20f, savedValue[6]);
                        Listeners6();
                        break;

                    case 7:
                        sliderArray[7].value = Mathf.InverseLerp(1000f, 5000f, savedValue[7]);
                        Listeners7();
                        break;

                    case 8:
                        sliderArray[8].value = Mathf.InverseLerp(1, 10, savedValue[8]);
                        Listeners8();
                        break;

                    case 9:
                        sliderArray[9].value = Mathf.InverseLerp(0.1f, 2f, savedValue[9]);
                        Listeners9();
                        break;

                    case 10:
                        sliderArray[10].value = Mathf.InverseLerp(0.1f, 2f, savedValue[10]);
                        Listeners10();
                        break;

                    case 11:
                        sliderArray[11].value = Mathf.InverseLerp(0, 100, savedValue[11]);
                        Listeners11();
                        break;

                    case 12:
                        sliderArray[12].value = Mathf.InverseLerp(1, 100, savedValue[12]);
                        Listeners12();
                        break;

                    case 13:
                        sliderArray[13].value = Mathf.InverseLerp(0.1f, 2f, savedValue[13]);
                        Listeners13();
                        break;

                    case 14:
                        sliderArray[14].value = Mathf.InverseLerp(5f, 15f, savedValue[14]);
                        Listeners14();
                        break;
                }
                i++;
            }
        }
        else
        {
            Reset0();
            Reset1();
            Reset2();
            Reset3();
            Reset4();
            Reset5();
            Reset6();
            Reset7();
            Reset8();
            Reset9();
            Reset10();
            Reset11();
            Reset12();
            Reset13();
            Reset14();
        }
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        Listeners0();
        Listeners1();
        Listeners2();
        Listeners3();
        Listeners4();
        Listeners5();
        Listeners6();
        Listeners7();
        Listeners8();
        Listeners9();
        Listeners10();
        Listeners11();
        Listeners12();
        Listeners13();
        Listeners14();
    }
}