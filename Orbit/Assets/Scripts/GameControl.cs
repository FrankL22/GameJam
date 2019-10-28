using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameControl : MonoBehaviour
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
    [SerializeField]
    private GameObject tree1;
    [SerializeField]
    private GameObject tree2;
    [SerializeField]
    private GameObject rock;

    public bool isDead = false;


    // Start is called before the first frame update
    void Start()
    {
        sphereCenter = GameObject.Find("Sphere").GetComponent<Transform>();
        myBlocks = new List<Transform>();
        for (int i = 0; i < 15; i++)
        {
            SpawnBlock();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Respawn();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }

    // Call to spawn a block in random location
    public void SpawnBlock()
    {
        if(!sphereCenter)
        {
            return;
        }

        Vector3 pos = GetRandomLocation();
        Vector3 upward = pos - sphereCenter.position;
        Vector3 forward = Quaternion.AngleAxis(Random.Range(0, 360), upward) * upward;
        int which = Random.Range(1, 9);
        GameObject newBlock = null;
        switch (which)
        {
            case 1:
                newBlock = Instantiate(fence1, pos, Quaternion.LookRotation(forward, upward));
                newBlock.transform.Rotate(transform.right, 90f);
                break;
            case 2:
                newBlock = Instantiate(fence2, pos, Quaternion.LookRotation(forward, upward));
                break;
            case 3:
                newBlock = Instantiate(fence3, pos, Quaternion.LookRotation(forward, upward));
                break;
            case 4:
                newBlock = Instantiate(tree1, pos, Quaternion.LookRotation(forward, upward));
                newBlock.transform.Rotate(transform.right, 90f);
                break;
            case 5:
                newBlock = Instantiate(tree2, pos, Quaternion.LookRotation(forward, upward));
                newBlock.transform.Rotate(transform.right, 90f);
                break;
            case 6:
            case 7:
            case 8:
                newBlock = Instantiate(rock, pos, Quaternion.LookRotation(forward, upward));
                newBlock.transform.Rotate(transform.right, 90f);
                break;
        }
        
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
        SpawnBlock();
    }

    public void ScorePoint()
    {
        if (!isDead)
        {
            points++;
            GameObject.FindGameObjectWithTag("display").GetComponent<UnityEngine.UI.Text>().text = points.ToString();
        }
    }

    public int GetPoints()
    {
        return points;
    }

    private void Respawn()
    {
        SceneManager.LoadScene("Demo");
    }
}
