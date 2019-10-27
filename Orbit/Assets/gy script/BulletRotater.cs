using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRotater : MonoBehaviour
{
	public Transform Planet;
	public float rotateSpeed = 10f;
	public System.Random rand;
	


	// Update is called once per frame
    void Start()
	{
        Planet = GameObject.Find("Sphere").GetComponent<Transform>();
        rand = new System.Random();

	}

	void FixedUpdate()
    {
		Vector3 a = transform.position - Planet.position;
		Vector3 rotateAxis = Vector3.Cross(a, transform.forward);
		transform.RotateAround(Planet.position, rotateAxis, rotateSpeed);
	}

	private void OnCollisionEnter(UnityEngine.Collision collision)
	{
		Debug.Log("********************");
		if (collision.gameObject.CompareTag("block"))
		{
			//other.gameObject.SetActive(false);
			//transform.Rotate(0, 0, -rand.Next(0, 90), Space.Self);

			
			ContactPoint contactPoint = collision.contacts[0];
			//Vector3 contactPt = contactPoint.point;
			Vector3 centerVec = (transform.position - Planet.position).normalized;


			Vector3 newDir = Vector3.zero;
			Vector3 curDir = transform.forward;
			newDir = Vector3.Reflect(curDir, contactPoint.normal);
			//newDir = (newDir - centerVec).normalized;

			Vector3 temp = Vector3.Cross(newDir, centerVec);
			Vector3 res = Vector3.Cross(centerVec, temp).normalized;

			Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, res);
			transform.rotation = rotation;

            collision.gameObject.SetActive(false);

		}
		if (collision.gameObject.CompareTag("Player"))
		{
			gameObject.SetActive(false);
		}
	}
    
    
    
    /*
	private void OnTriggerEnter(UnityEngine.Collider other)
	{
		if (other.gameObject.CompareTag("Block"))
		{
			//other.gameObject.SetActive(false);
			Debug.Log("********************");
			transform.Rotate(0, 0, -rand.Next(0, 90), Space.Self);
		}
		if (other.gameObject.CompareTag("Player"))
		{
			Debug.Log("********************");
			gameObject.SetActive(false);
		}
	}
    */



}
