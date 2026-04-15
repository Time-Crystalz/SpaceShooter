using System;
using System.Collections;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    private float deathAnimationDuration = 0.433f;
    private Animator animator;
    private Enemy enemy;
    private Rigidbody2D rigidbody2D;
    public event EventHandler OnEnemyDeath;
    [SerializeField] private AudioClip enemyDied1;
    [SerializeField] private AudioClip enemyDied2;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        enemy = GetComponent<Enemy>();
    }
    void OnEnable()
    {
        EventBus.Suscribe(EventBus.events.OnEnemyDeath, On_EnemyDeath);
    }

    void OnDisable()
    {
        EventBus.UnSuscribe(EventBus.events.OnEnemyDeath, On_EnemyDeath);
    }

    private void On_EnemyDeath(GameObject sender)
    {
        if (gameObject == sender)
        {
            OnEnemyDeath?.Invoke(this, EventArgs.Empty);

            if (UnityEngine.Random.value > 0.5)
            {

                AudioManager.Instance.PlaySound(enemyDied1);
            }

            else
            {
                AudioManager.Instance.PlaySound(enemyDied2);
                
            }

            StartCoroutine(DeathSequence());

        }

    }

    private IEnumerator DeathSequence()
    {
        // Play death animation
        animator.Rebind();
        animator.Update(0f);
        rigidbody2D.bodyType = RigidbodyType2D.Static;
        animator.Play("blastAnimation", 0, 0f);

        yield return new WaitForSeconds(deathAnimationDuration);
        if (animator == null || rigidbody2D == null || enemy == null) yield break;
        // Reset rigidbody for when enemy is reused from pool
        rigidbody2D.bodyType = RigidbodyType2D.Dynamic;

        // Return to pool instead of destroying
        if (enemy.Spawner != null)
        {
            Debug.Log("Calling ReturnToPool!");
            enemy.Spawner.ReturnToPool(gameObject);
        }
        else
        {
            Debug.LogWarning("Spawner is NULL on this enemy!");
        }
    }
}

