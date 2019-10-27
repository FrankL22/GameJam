using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BlockBehavior : NetworkBehaviour
{
    private Rigidbody rigid;
    private GameControl game;
    [SerializeField]
    private GameObject smokeParticle;
    [SerializeField]
    private float destroyTime = 3.0f;
    [SerializeField]
    private int hitpoint = 1;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        gameObject.tag = "block";
        game = GameObject.FindObjectOfType<GameControl>();
        GameObject smoke = Instantiate(smokeParticle, transform.position, transform.rotation);
        NetworkServer.Spawn(smoke);
        Destroy(smoke, 2.0f);
        Destroy(gameObject, destroyTime);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody != null && collision.rigidbody.gameObject.CompareTag("bullet"))
        {
            hitpoint--;
        }

        if (hitpoint == 0)
        {
            game.ScorePoint();
            Destroy(gameObject, destroyTime);
        }
    }

    private void OnDestroy()
    {
        if (game != null)
        {
            game.DestroyBlock(transform);
            game.SpawnBlock();
        }
    }
}
