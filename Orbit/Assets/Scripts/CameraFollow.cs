using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector3 worldUp = target.up * offset.y;
            //Vector3 worldForward = target.TransformDirection(Vector3.forward) * offset.z;
            //transform.position = Vector3.Lerp(transform.position, target.position + worldUp, 10*Time.deltaTime);
            transform.position = target.position + worldUp;
            //transform.LookAt(target);
            transform.rotation = Quaternion.LookRotation(-target.up, target.forward);
        }
    }
}
