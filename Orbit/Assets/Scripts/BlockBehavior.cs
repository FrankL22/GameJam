using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehavior : MonoBehaviour
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
        rigid.detectCollisions = true;
        gameObject.tag = "block";
        game = GameObject.FindObjectOfType<GameControl>();
        if (smokeParticle != null)
        {
            GameObject smoke = Instantiate(smokeParticle, transform.position, transform.rotation);
            Destroy(smoke, 2.0f);
        }
    }

    public void Hit()
    {
        if (game != null)
        {
            game.DestroyBlock(transform);
            game.ScorePoint();
            Destroy(gameObject, destroyTime);
        }
    }
}
