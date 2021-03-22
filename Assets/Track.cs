using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    [SerializeField] private Transform ground;
    private float off = 0;

    private void Update()
    {
        off += Time.deltaTime * 2;
        rainbow.mainTextureOffset = new Vector2(12.6f, off);
    }

    private Material rainbow;

    private void Awake()
    {
        //truckGenerator = GameObject.FindGameObjectWithTag("TG");
        //tg = GameObject.FindGameObjectWithTag("tg");
        //tg = GameObject.FindGameObjectWithTag("tg");
        StartCoroutine(AwakeDelay());
        ground.position = new Vector3(4.1f, -6, chunkLenght - 59.3f);
        rainbow = GetComponent<MeshRenderer>().material;
    }

    private IEnumerator AwakeDelay()
    {
        yield return new WaitForSeconds(1);
        CreateBezierPoints();
        StartCoroutine(CreateTrack());
    }

    //GameObject truckGenerator;
    public int bezierCurveAmount; //whereas 1 is cubic curve (the amount of curvatures)

    public int level;

    public List<Vector3> points;

    [SerializeField]
    public float xAxis, zAxis;

    private int triangleIndex;

    [SerializeField] //a's are coordinates for our x bezier, all the rest for lerp
    public Vector3 a0,
     a1,
     a2,
     a3,
     b0,
     b1,
     b2,
     c0,
     c1,
     d0;

    [SerializeField]//a's are coordinates for our x bezier, all the rest for lerp
    public Vector3
     a01,
     a11,
     a21,
     a31,
     a41,
     a51,
     b01,
     b11,
     b21,
     b31,
     b41,
     c01,
     c11,
     c21,
     c31,
     d01,
     d11,
     d21,
     f01,
     f11,
     g01;

    public bool track;

    private float t = 0; // x axis distance between vertices
    private float t0 = 0; // z axis distance between vertices
    private float t1 = 0; // t1 and t2 operational mediators
    private float t2 = 0;

    private Vector3 VertexCoordinateXY() // gives back x axis bezier coordinaes
    {
        if (t1 == 2) // increses x axis vertex coordinate
        {
            t += 1 / xAxis; // the distance between verteces x axis
            t1 = 0;
            if (t > 1) // when 1 row at x axis fills up
            {
                t = 0; // the lerp t
            }
        }
        b0 = new Vector3(Mathf.Lerp(a0.x, a1.x, t), Mathf.Lerp(a0.y, a1.y, t), 0);
        b1 = new Vector3(Mathf.Lerp(a1.x, a2.x, t), Mathf.Lerp(a1.y, a2.y, t), 0);
        b2 = new Vector3(Mathf.Lerp(a2.x, a3.x, t), Mathf.Lerp(a2.y, a3.y, t), 0);
        c0 = new Vector3(Mathf.Lerp(b0.x, b1.x, t), Mathf.Lerp(b0.y, b1.y, t), 0);
        c1 = new Vector3(Mathf.Lerp(b1.x, b2.x, t), Mathf.Lerp(b1.y, b2.y, t), 0);
        d0 = new Vector3(Mathf.Lerp(c0.x, c1.x, t), Mathf.Lerp(c0.y, c1.y, t), 0); // the x bezier
        t1++; // increses x axis vertex coordinate

        return d0;
    }

    [SerializeField]
    public List<Vector3> BezierHandles;

    private List<Vector3> BezierInterpolations;
    public int rZ;
    public int chunkLenght;

    [SerializeField]
    private float bFloat;

    public float chunkLenghtEnviroment = 0;
    public float zz;
    public float randomizeBridge = 1;

    private void CreateBezierPoints()
    {
        BezierHandles = new List<Vector3>();
        BezierHandles.Add(a01);
        bFloat = chunkLenght;

        //zAxis = -chunkLenght / System.Convert.ToInt32(Mathf.Lerp(20, 5, chunkLenghtEnviroment));

        //zAxis = 50;
        //if (zAxis < 0)
        //{
        //    zAxis = bFloat / 2 + chunkLenght % 2;
        //}
        //float bFloat = 60;
        zz = -chunkLenght / 8;

        for (int i = 1; i < bezierCurveAmount + 1; i++) // number of verts
        {
            BezierHandles[0] = new Vector3(0, 0, 0); //initial point + lenght
            BezierHandles.Add(new Vector3(0, randomizeBridge * zz, bFloat / 4 * 3)); //initial point + lenght
            BezierHandles.Add(new Vector3(0, zz / randomizeBridge, bFloat / 4));
            BezierHandles.Add(new Vector3(0, 0, bFloat));
            //BezierHandles.Add(new Vector3(0, 0, -16));

            //BezierHandles.Add(new Vector3(0, UnityEngine.Random.Range(-100,100), UnityEngine.Random.Range(-100, 100)));
            //BezierHandles.Add(new Vector3(0, 0, 5));
        };
        //totalLerpCount = ((BezierHandles.Count * (BezierHandles.Count + 1)) / 2) - BezierHandles.Count + (BezierHandles.Count - 2);
        extraListLenght = ((BezierHandles.Count * (BezierHandles.Count + 1)) / 2);
        extraListLenghtBack = extraListLenght - BezierHandles.Count;
        handlecount = BezierHandles.Count;
        gapCounter = handlecount;
        gapFiller = handlecount - 1;
        //Debug.Log(((BezierHandles.Count * (BezierHandles.Count + 1)) / 2));
        for (int i = handlecount; i < extraListLenght; i++)
        {
            BezierHandles.Add(new Vector3(0, 0, 0)); //initial point + lenght
        }
    }

    //int bezierHandleIndex = 0;

    private int handlecount;
    private int totalLerpCount;
    private int extraListLenght;
    private int extraListLenghtBack;
    private int gapCounter;
    private int gapFiller;

    private Vector3 VertexCoordinateYZ() // gives back z axis bezier coordinaes
    {
        for (int i = 0; i < extraListLenghtBack; i++)
        {
            if (BezierHandles[0] != null) BezierHandles.RemoveAt(4); //initial point + lenght
                                                                     //if (BezierHandles[i] != null) Debug.Log(" Removing " + BezierHandles[i]);
        }
        gapCounter = handlecount;
        gapFiller = gapCounter - 1;
        if (t2 == (xAxis + 1) * 2) // increses x axis vertex coordinate
        {
            t0 += 1 / zAxis; // the distance between verteces z axis
            t2 = 0;
            //t3 += 1/ (zAxis + 1000);
        }

        for (int i = 0; BezierHandles.Count < extraListLenght; i++)
        {
            if (i == gapFiller)
            {
                if (gapCounter >= 3)
                {
                    //Debug.Log("gap counter is " + gapCounter);
                    //Debug.Log("gap filler is " + gapFiller);
                    gapCounter--;
                    gapFiller += gapCounter;
                    i++;
                }
            }
            BezierHandles.Add(new Vector3(0, Mathf.Lerp(BezierHandles[i].y, BezierHandles[i + 1].y, t0), Mathf.Lerp(BezierHandles[i].z, BezierHandles[i + 1].z, t0)));
            //Debug.Log(BezierHandles.Count);

            //Debug.Log("i is " + i);
            //Debug.Log(t0);
        }
        t2++;
        //Debug.Log("------------------");
        //Debug.Log(BezierHandles[BezierHandles.Count - 1]);
        return BezierHandles[BezierHandles.Count - 1];
    }

    //Vector3 aaa, aaa1, aaa2, aaa3, aaa4, aaa5, aaa6, aaa7, aaa8;
    private int Triangles() // gives back the triangle indicies
    {
        int x = Convert.ToInt32(xAxis);
        int result = 0;
        int row;

        int triangleNo = ((triangleIndex - (triangleIndex % 3) + 3) / 3); // starts from 1
        if (triangleNo % (2 * x) != 0)
        {
            row = (triangleNo - (triangleNo % (2 * x))) / (2 * x);
        }
        else
        {
            row = triangleNo / (2 * x) - 1;
        }
        if (triangleNo % 2 != 0)
        {
            switch (triangleIndex % 3)
            {
                case 0:
                    result = ((triangleNo - (row * (2 * x)) - 1) / 2) + (row * (x + 1));
                    break;

                case 1:
                    result = ((triangleNo - (row * (2 * x)) - 1) / 2) + (row * (x + 1)) + (x + 1);
                    break;

                case 2:
                    result = ((triangleNo - (row * (2 * x)) - 1) / 2) + (row * (x + 1)) + 1;
                    break;
            }
        }
        else
        {
            switch (triangleIndex % 3)

            {
                case 0:
                    result = ((triangleNo - (row * (2 * x))) / 2) + (row * (x + 1));
                    break;

                case 1:
                    result = ((triangleNo - (row * (2 * x))) / 2) + (row * (x + 1)) + (x + 1) - 1;
                    break;

                case 2:
                    result = ((triangleNo - (row * (2 * x))) / 2) + (row * (x + 1)) + (x + 1);
                    break;
            }
        }
        //return triIndices[triangleIndex];

        return result;
    }

    private IEnumerator CreateTrack()
    {
        //AcquireVertecies();
        Mesh mesh = new Mesh();
        mesh.name = "The Gay Bridge";
        Vector2[] uvVector2 = new Vector2[Convert.ToInt32((xAxis + 1) * (zAxis + 1))];
        points = new List<Vector3>();
        for (int i = 0; i < ((xAxis + 1) * (zAxis + 1)); i++) // number of verts
        {
            points.Add(new Vector3(VertexCoordinateXY().x, VertexCoordinateXY().y + VertexCoordinateYZ().y, VertexCoordinateYZ().z));
            uvVector2[i] = new Vector2(t, t0);
        };

        //points[0] = new Vector3(points[1].x, points[1].y, points[1].z);
        //points[Convert.ToInt32((xAxis + 2) * (zAxis + 1)) - 1] = new Vector3(points[0].x, points[0].y, points[0].z + 30);
        yield return new WaitForEndOfFrame();

        int[] triIndices = new int[Convert.ToInt32((xAxis * zAxis * 2 * 3))];
        for (triangleIndex = 0; triangleIndex < triIndices.Length; triangleIndex++)
        {
            triIndices[triangleIndex] = Triangles();
        };
        List<Vector3> normals = new List<Vector3>();
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < ((xAxis + 1) * (zAxis + 1)); i++) // number of verts
        {
            normals.Add(new Vector3(1, 0, 0));
        };

        mesh.SetVertices(points);
        mesh.uv = uvVector2;
        mesh.SetNormals(normals);

        mesh.triangles = triIndices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.Optimize();
        GetComponent<MeshFilter>().sharedMesh = mesh;
        gameObject.AddComponent<MeshCollider>().convex = false;
        MeshRenderer a = GetComponent<MeshRenderer>();
        a.useLightProbes = false;
        a.receiveShadows = false;
        a.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        a.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
    }
}