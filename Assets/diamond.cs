using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diamond : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Player player;

    public void Collected()
    {
        StartCoroutine(Collect());
    }

    private void Start()
    {
        transform.position = new Vector3(Random.Range(7, -17), transform.position.y, Random.Range(3, 148));
    }

    private Vector3 initial1;
    private Vector3 initial2;
    private Vector3 initial3;
    private float t = 0;

    private IEnumerator Collect()
    {
        initial1 = transform.position;
        initial2 = transform.localScale;
        float z = 2;
        while (t < 1)
        {
            t += Time.deltaTime * z;
            z -= Time.deltaTime * 1.99f;
            transform.position = Vector3.Lerp(initial1, target.position, t);
            transform.localScale = Vector3.Lerp(initial2, target.localScale, t);
            yield return new WaitForEndOfFrame();
        }
        initial3 = target.localScale;
        float x = 2f;
        Vector3 v3 = new Vector3(initial3.x * x, initial3.y * x, initial3.z * x);
        player.AddDiamond();
        while (t < 1)
        {
            t += Time.deltaTime * 2;

            target.localScale = Vector3.Lerp(initial3, v3, t);
            yield return new WaitForEndOfFrame();
        }
        while (t > 0)
        {
            t -= Time.deltaTime * 2;

            target.localScale = Vector3.Lerp(initial3, v3, t);
            yield return new WaitForEndOfFrame();
        }
        this.gameObject.SetActive(false);
    }
}