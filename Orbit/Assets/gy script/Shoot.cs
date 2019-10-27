using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Rigidbody Bullet;
	public Transform ShootPoint;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
		{
			Rigidbody clone;
			clone = (Rigidbody) Instantiate(Bullet, ShootPoint.position, ShootPoint.rotation);
		}
    }
}
