using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Shoot : NetworkBehaviour
{
    public GameObject Bullet;
	public Transform ShootPoint;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isLocalPlayer)
		{
            CmdShoot();
		}
    }

    //[Command]
    void CmdShoot()
    {
        GameObject bullet = Instantiate(Bullet, ShootPoint.position, ShootPoint.rotation);
        NetworkServer.Spawn(bullet);
    }
}
