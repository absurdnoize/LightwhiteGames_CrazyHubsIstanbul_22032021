using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Track bridge;

    [SerializeField] private GameObject barrel;

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
    private Vector3 center;
    public int barrelQuantity;

    private void Start()
    {
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
        bFloat = bridge.chunkLenght;
        chunkLenght = bridge.chunkLenght;
        z = bridge.zz;
        a = bridge.BezierHandles[0];
        b = bridge.BezierHandles[1];
        c = bridge.BezierHandles[2];
        d = bridge.BezierHandles[3];
        center = LerpMovement(0.5f) + Vector3.up * 5;
        for (int i = 0; i < -chunkLenght / barrelQuantity; i++)
        {
            if (i >= -chunkLenght / (barrelQuantity * 2))
            {
                barrel.GetComponent<barrel>().back = true;
                barrel.GetComponent<barrel>().end = chunkLenght + 10;
            }
            else
            {
                barrel.GetComponent<barrel>().back = false;
                barrel.GetComponent<barrel>().end = -10;
            }
            barrel.GetComponent<barrel>().center = center;
            Instantiate(barrel, LerpMovement(Mathf.InverseLerp(0, -chunkLenght / barrelQuantity, i)), Quaternion.identity, this.transform);
        }
    }

    private Vector3 LerpMovement(float t)
    {
        Vector3 ab = new Vector3(0, Mathf.Lerp(a.y, b.y, t), Mathf.Lerp(a.z, b.z, t));
        Vector3 bc = new Vector3(0, Mathf.Lerp(b.y, c.y, t), Mathf.Lerp(b.z, c.z, t));
        Vector3 cd = new Vector3(0, Mathf.Lerp(c.y, d.y, t), Mathf.Lerp(c.z, d.z, t));
        Vector3 abbc = new Vector3(0, Mathf.Lerp(ab.y, bc.y, t), Mathf.Lerp(ab.z, bc.z, t));
        Vector3 bccd = new Vector3(0, Mathf.Lerp(bc.y, cd.y, t), Mathf.Lerp(bc.z, cd.z, t));
        Vector3 abbcbccd = new Vector3(Random.Range(-3, -9), Mathf.Lerp(abbc.y, bccd.y, t) - 3, Mathf.Lerp(abbc.z, bccd.z, t));
        return abbcbccd;
    }

    // Update is called once per frame
    private void Update()
    {
    }
}