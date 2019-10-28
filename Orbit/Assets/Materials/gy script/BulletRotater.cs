using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletRotater : MonoBehaviour
{
	public Transform Planet;
	public float rotateSpeed = 10f;
	public System.Random rand;
    [SerializeField] public GameObject gameOver;
    [SerializeField] public GameObject score;

    // Update is called once per frame
    void Start()
	{
		rand = new System.Random();
        Planet = GameObject.Find("Sphere").GetComponent<Transform>();
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
            collision.gameObject.GetComponent<BlockBehavior>().Hit();

		}
		if (collision.gameObject.CompareTag("Player") && !GameObject.FindObjectOfType<GameControl>().isDead)
		{
            GameObject.FindObjectOfType<GameControl>().isDead = true;
            GameObject.FindGameObjectWithTag("Finish").GetComponent<Text>().text = "Game Over";
            GameObject.FindGameObjectWithTag("Respawn").GetComponent<Text>().text = "Press Space to respawn";
            GameObject.FindGameObjectWithTag("score").GetComponent<Text>().text = "Score: " + GameObject.FindObjectOfType<GameControl>().GetPoints();
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
