using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // camera will follow this object
    public Transform Target;

    public Transform Target2;

    //camera transform
    public Transform camTransform;

    // offset between camera and target
    public Vector3 Offset;

    // change this value to get desired smoothness
    public float SmoothTime = 0.3f;

    public float LookOffset = 0.3f;

    // This value will change at the runtime depending on target movement. Initialize with zero vector.
    private Vector3 velocity = Vector3.zero;

    public bool phase1 = true;
    public bool phase2 = false;
    public bool phase3 = false;

    private void Start()
    {
        //Offset = camTransform.position - Target.position;
    }

    public void Phase2()
    {
        phase1 = false;
        phase2 = true;
        Offset = new Vector3(0, 2, 3.5f);
        SmoothTime = 0.05f;
    }

    public void Phase3()
    {
        phase2 = false;
        phase3 = true;
    }

    private void LateUpdate()
    {
        // update position
        if (phase1)
        {
            Vector3 targetPosition = Target2.position + Offset;
            camTransform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, SmoothTime);
            transform.LookAt(new Vector3(Target.transform.position.x, LookOffset, Target.transform.position.z));
        }
        // update rotation
        if (phase2)
        {
            Vector3 targetPosition = new Vector3(Target.position.x + Offset.x, Target2.position.y + Offset.y, Target2.position.z + Offset.z);
            camTransform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, SmoothTime);
            transform.localEulerAngles = new Vector3(Target.transform.localEulerAngles.x + 20, 180, transform.localEulerAngles.z);
        }
        if (phase3)
        {
            transform.position = new Vector3(transform.position.x - Time.deltaTime / 10, transform.position.y + Time.deltaTime / 10, transform.position.z + Time.deltaTime / 10);
            transform.LookAt(Target);
        }
    }
}