using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    // Use this for initialization
    [SerializeField] private float t;

    private void Start()
    {
        StartCoroutine(Rotate(t));
    }

    private IEnumerator Rotate(float duration)
    {
        while (true)
        {
            float startRotation = transform.eulerAngles.z;
            float endRotation = startRotation + 360.0f;
            float t = 0.0f;
            while (t < duration)
            {
                t += Time.deltaTime;
                float yRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, yRotation);
                yield return null;
            }
        }
    }
}