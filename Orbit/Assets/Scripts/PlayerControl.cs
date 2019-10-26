using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private bool isGrounded;
    private float deltaGround = 0.2f; // character is grounded up to this distance
    private Vector3 surfaceNormal; // current surface normal
    private Vector3 myNormal; // character normal
    private float mySpeed;
    private float distGround; // distance from character position to ground

    [SerializeField]
    private float moveSpeed = 20.0f;
    private float turnSpeed = 125; // turning speed (degrees/second)
    private float gravity = 12.5f; // gravity acceleration
    private float jumpSpeed = 15; // vertical jump initial speed
    private Rigidbody rigid;
    private Transform myTransform;
    private BoxCollider boxCollider; // drag BoxCollider ref in editor

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        rigid = GetComponent<Rigidbody>();
        myNormal = transform.up; // normal starts as character up direction
        myTransform = transform;
        rigid.freezeRotation = true; // disable physics rotation
        distGround = boxCollider.size.y - boxCollider.center.y;// distance from transform.position to ground
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // apply constant weight force according to character normal:
        rigid.AddForce(-gravity * rigid.mass * myNormal);
        mySpeed = rigid.velocity.magnitude;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rigid.AddForce(Vector3.up * moveSpeed);
        }
    }
}
