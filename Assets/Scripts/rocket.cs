using System;
using UnityEngine;

public class rocket : MonoBehaviour
{


    private Rigidbody2D rigidbody2D;
    private Vector2 direction;
    private float rocketSpeed = 150f;
    private float maxTrackTime = 10f;
    private float xBoundary = 17f;
    private float yBoundary = 10f;
    private int rocketDamage = 200;
    public event EventHandler enemyDieByRocket;


    private void Awake()
    {
        TryGetComponent<Rigidbody2D>(out rigidbody2D);
    }
    private void Start()
    {
        rigidbody2D.gravityScale = 0f;

    }


    private void Update()
    {
        RocketGoesOffTheScreen();
    }

    private void FixedUpdate()
    {


        maxTrackTime = maxTrackTime - Time.fixedDeltaTime;
        if (maxTrackTime > 0f)
        {
            if (player.Instance == null)
            {
                return;
            }

            direction = (Vector2)(player.Instance.transform.position - transform.position).normalized;
            EnemyRotation();
            rigidbody2D.AddForce(direction * rocketSpeed * Time.fixedDeltaTime);

        }

        else
        {
            rigidbody2D.AddForce(direction * rocketSpeed * Time.fixedDeltaTime);
        }
    }



    private void RocketGoesOffTheScreen()
    {


        float x = transform.position.x;
        float y = transform.position.y;

        if (x < -xBoundary)
        {

            Destroy(gameObject);
        }
        if (x > xBoundary)
        {
            Destroy(gameObject);
        }
        if (y > yBoundary)
        {
            Destroy(gameObject);
        }
        if (y < -yBoundary)
        {
            Destroy(gameObject);
        }
    }

    private void EnemyRotation()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.TryGetComponent<player>(out player player);
        other.TryGetComponent<Enemy>(out Enemy enemy);

        if (player)
        {
            other.gameObject.TryGetComponent<Health>(out Health health);
            if (health)
            {
                health.TakeDamage(rocketDamage,gameObject);
                Destroy(gameObject);
            }
        }

        if (enemy)
        {
            other.TryGetComponent<Health>(out Health health);
            if (health)
            {
                enemyDieByRocket?.Invoke(this,EventArgs.Empty);
                health.TakeDamage(rocketDamage,gameObject);
                Destroy(gameObject);
            }
        }
    }


}
