using System;

using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Vector3 newPosition;
    private float x;
    private float y;
    private float x2;
    private float y2;

    private float originalX;
    private float originalY;
    private EnemyDetection enemyDetection;
    private Rigidbody2D EnemyBody;
    [SerializeField] private float forceValue = 250f;
    private bool PLayerLocated = false;
    private bool stopMoving = false;

    private void Awake()
    {
        enemyDetection = GetComponentInChildren<EnemyDetection>();
        TryGetComponent<Rigidbody2D>(out EnemyBody);
    }
    private void Start()
    {
        enemyDetection.StopOnplayerLoacted += Stop_OnPlayerLocated;
        enemyDetection.MoveOnplayerLost += Move_OnPlayerLost;
        enemyDetection.OnPlayerLocatedEvent += DoOnPLayerLocated;
        FindNewPosition();
       

        originalX = UnityEngine.Random.Range(-15, 15);
        originalY = UnityEngine.Random.Range(-8.63f, 8.63f);

    }



    private void Update()
    {

        MoveEnemey();

    }

    private void FindNewPosition()
    {
        if (stopMoving)
        {
            return;
        }

        else
        {


            x = UnityEngine.Random.Range(10f, 15f);
            x2 = UnityEngine.Random.Range(-15f, -10f);

            y = UnityEngine.Random.Range(6f, 8.63f);
            y2 = UnityEngine.Random.Range(-8.63f, -6f);

            originalX = UnityEngine.Random.Range(x2, x);
            originalY = UnityEngine.Random.Range(y2, y);


            newPosition = new Vector3(originalX, originalY);
        }


    }

    private void EnemyRotation()
    {
        Vector2 direction = (Vector2)(newPosition - transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 3f * Time.deltaTime);
    }

    private void Move_OnPlayerLost(object sender, EventArgs eventArgs)
    {

        PLayerLocated = false;
        stopMoving = false;

    }

    private void Stop_OnPlayerLocated(object sender, EventArgs e)
    {
        PLayerLocated = true;
    }

    private void DoOnPLayerLocated(object sender, EnemyDetection.OnPlayerLocatedEventArgs onPlayerLocatedEventArgs)
    {
        stopMoving = true;
        newPosition = onPlayerLocatedEventArgs.playerPosition;
    }

    private void MoveEnemey()
    {
        if (PLayerLocated == true)
        {

            return;
        }
        else
        {
            EnemyRotation();
            float distance = Vector3.Distance(transform.position, newPosition);
            if (distance < 2f)
            {

                FindNewPosition();
            }

            else
            {

                EnemyBody.AddForce(forceValue * transform.up * Time.deltaTime);
            }

        }
    }
}
