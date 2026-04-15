using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class boss : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private Vector3 spawnPoint = new Vector3();
    [SerializeField] private Vector3 bossPositon = new Vector3();
    private Rigidbody2D rigidbody2D;
    private bool isMoving;
    private float speed = 2f;
    
   
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.gravityScale = 0f;
        rigidbody2D.linearDamping = 0.9f;
        gameObject.SetActive(false);
        
    }
    private void Start()
    {
        
    }

    private void Update()
    {
         if (isMoving)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                bossPositon,
                speed * Time.deltaTime
            );

            if (transform.position == bossPositon)
            {
                isMoving = false;
                EventBus.InvokeEvent(EventBus.events.OnBossArrival);

            }
        }
    }

    public void BossAppear()
    {
        transform.position = spawnPoint;
        gameObject.SetActive(true);
        animator.Play("bossAnimation");
        isMoving = true;
    }

}
