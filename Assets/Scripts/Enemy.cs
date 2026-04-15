
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D enemyBody;
    public EnemySpawner Spawner;

    


    private void Awake()
    {
        enemyBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        enemyBody.gravityScale = 0f;
        enemyBody.linearDamping = 0.9f;



    }



    public virtual void OnPlayerLocated(player playerbody)
    {

    }



}
