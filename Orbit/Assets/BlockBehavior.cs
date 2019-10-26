using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehavior : MonoBehaviour
{
    private Rigidbody rigid;
    [SerializeField]
    private float destroyTime = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        gameObject.tag = "block";
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody != null && collision.rigidbody.gameObject.CompareTag("bullet"))
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>().ScorePoint();
            Destroy(gameObject, destroyTime);
        }
    }
}
