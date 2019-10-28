using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCharacterController : MonoBehaviour
{
    public float Speed = 10f;
    public float RotationSpeed = 250f;
    public Vector3 moveAmount;
    public Vector3 smoothMoveVelocity;
    float mouseX;
    private bool wasMoving = false;

    private void FixedUpdate()
    {
        if (GameObject.FindObjectOfType<GameControl>().isDead)
        {
            return;
        }

        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        Vector3 targetMoveAmount = moveDir * Speed;
        /*if (moveAmount.magnitude > 0.00000001f && !wasMoving)
        {
            transform.GetChild(1).transform.localScale = new Vector3(1f, 1f, 1f);
            transform.GetChild(0).transform.localScale = new Vector3(0f, 0f, 0f);
            wasMoving = true;
        }
        else if (moveAmount.magnitude < 0.00000001f && wasMoving)
        {
            transform.GetChild(0).transform.localScale = new Vector3(1f, 1f, 1f);
            transform.GetChild(1).transform.localScale = new Vector3(0f, 0f, 0f);
            Debug.Log("stopped moving");
            wasMoving = false;
        }*/
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
        //mouseX += Input.GetAxis("Mouse X") * RotationSpeed;
        //GetComponent<Rigidbody>().rotation = Quaternion.Euler(0, mouseX, 0);
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * RotationSpeed);
        //Debug.Log("body's forward is " +  transform.forward);
       
    }
}
