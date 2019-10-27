using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameControl : NetworkBehaviour
{
    private Transform sphereCenter;
    private List<Transform> myBlocks;
    private int points = 0;
    [SerializeField]
    private float spawnMargin = 20;
    [SerializeField]
    private GameObject fence1;
    [SerializeField]
    private GameObject fence2;
    [SerializeField]
    private GameObject fence3;

    // Start is called before the first frame update
    void Start()
    {
        sphereCenter = GameObject.Find("Sphere").GetComponent<Transform>();
        myBlocks = new List<Transform>();
        for (int i = 0; i < 10; i++)
        {
            SpawnBlock();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Call to spawn a block in random location
    public void SpawnBlock()
    {
        Vector3 pos = GetRandomLocation();
        Vector3 upward = pos - sphereCenter.position;
        Vector3 forward = Quaternion.AngleAxis(Random.Range(0, 360), upward) * upward;
        int which = Random.Range(1, 4);
        GameObject newBlock = null;
        switch (which)
        {
            case 1:
                newBlock = Instantiate(fence1, pos, Quaternion.LookRotation(forward, upward));
                break;
            case 2:
                newBlock = Instantiate(fence2, pos, Quaternion.LookRotation(forward, upward));
                break;
            case 3:
                newBlock = Instantiate(fence3, pos, Quaternion.LookRotation(forward, upward));
                break;
        }
        NetworkServer.Spawn(newBlock);
        myBlocks.Add(newBlock.transform);
    }

    private Vector3 GetRandomLocation()
    {
        Vector3 randV = new Vector3(0, 0, 0);
        bool valid = false;
        while(!valid)
        {
            randV = sphereCenter.position;
            randV += Random.onUnitSphere * (sphereCenter.localScale.x / 2 - 0.3f);
            valid = true;
            foreach (Transform obj in myBlocks)
            {
                Vector3 dist = obj.position - randV;
                if (dist.magnitude <= obj.localScale.x + spawnMargin)
                {
                    valid = false;
                }
            }
        }

        return randV;
    }

    public void DestroyBlock(Transform block)
    {
        myBlocks.Remove(block);
    }

    public void ScorePoint()
    {
        points++;
    }

    public int GetPoints()
    {
        return points;
    }
}
