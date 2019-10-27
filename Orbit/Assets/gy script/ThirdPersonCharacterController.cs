using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ThirdPersonCharacterController : NetworkBehaviour
{
    public float Speed = 10f;
    public float RotationSpeed = 250f;
    public Vector3 moveAmount;
    public Vector3 smoothMoveVelocity;
    float mouseX;

    private void Start()
    {
        if (isLocalPlayer)
        {
            GameObject.Find("Camera").GetComponent<CameraFollow>().target = gameObject.transform;
        }
    }
    public override void OnStartLocalPlayer()
    {
       
        //设置己方颜色
        MeshRenderer temp = GetComponent<MeshRenderer>();
        temp.material.color = Color.red;
    }    

    private void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
            Vector3 targetMoveAmount = moveDir * Speed;
            moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);
            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
            //mouseX += Input.GetAxis("Mouse X") * RotationSpeed;
            //GetComponent<Rigidbody>().rotation = Quaternion.Euler(0, mouseX, 0);
            transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * RotationSpeed);
            //Debug.Log("body's forward is " +  transform.forward);
        }
       
    }

}
