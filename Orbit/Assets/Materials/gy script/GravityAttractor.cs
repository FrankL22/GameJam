using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAttractor : MonoBehaviour
{
    // Start is called before the first frame update
    public float gravity = -10f;

    public void Attract(Transform body)
    {
        Vector3 targetDir = (body.position - transform.position).normalized;
        Vector3 bodyUp = body.up;
      

        body.rotation = Quaternion.FromToRotation(bodyUp, targetDir) * body.rotation;
        //body.GetComponent<Rigidbody>().rotation = Quaternion.FromToRotation(bodyUp, targetDir) * body.rotation; ;
        body.GetComponent<Rigidbody>().AddForce(gravity * targetDir);
    }
}
