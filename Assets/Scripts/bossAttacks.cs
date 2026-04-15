using System;
using System.Collections;
using UnityEngine;

public class bossAttacks : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private AudioClip electricGunClip;
    private Health health;
    private Bossdetection bossdetection;
    private bool hasPlayed = false;

    private float BossAttackEffectMaxTime = 3f;

    public bool IfPlayerEnters = false;
    private int bossHealth;
    public static event EventHandler FireRocket;
    private float rocketFiringTimer = 0;
    private Coroutine slowEffect; // add this field at the top
    private float rocketFiringMaxTime = 5f;





    private void Awake()
    {


    }


    private void Start()
    {
        bossdetection = GetComponentInParent<Bossdetection>();
        TryGetComponent<Health>(out health);

        spriteRenderer.enabled = false;
        bossdetection.OnPlayerEnters += On_PlayerEnters;
    }


    private void Update()
    {




        if (IfPlayerEnters && !hasPlayed)
        {
            hasPlayed = true;
            StartCoroutine(PlayOnce());
        }

        if (!IfPlayerEnters)
        {

            hasPlayed = false;
        }
        SpecialAttak();

        rocketFiringTimer += Time.deltaTime;
        if (rocketFiringTimer >= rocketFiringMaxTime)
        {
            Debug.Log("Time to Fire the Rocket");
            FireMissile();
            rocketFiringTimer = 0f;
        }


    }
    

    private void SpecialAttak()
    {
        if (health == null)
        {
            return;
        }
        if (health.currentHealth < 200)
        {
            if (UnityEngine.Random.value < 0.3)
            {
                EnemySpawner.Instance.SpawnWave();
            }
        }
    }

    private void FireMissile()
    {
        if (player.Instance == null)
        {
            return;
        }
        if (UnityEngine.Random.value < 0.7)
        {
            Debug.Log("RocketFiring Event Fired");
            FireRocket?.Invoke(this, EventArgs.Empty);
        }
    }


    private void On_PlayerEnters(object sender, Bossdetection.OnPlayerDetectedEventArgs playerArgs)
    {
        playerArgs.playerHealth.TakeDamage(10, gameObject);

        // Cancel previous slow if still running
        if (slowEffect != null)
        {
            StopCoroutine(slowEffect);
        }
        slowEffect = StartCoroutine(PlayerEffectByAttack(playerArgs.playerRigidBody));
    }

    private IEnumerator PlayerEffectByAttack(Rigidbody2D playerRb)
    {
        if (playerRb == null) yield break;

        playerRb.linearDamping = 5f;

        yield return new WaitForSeconds(BossAttackEffectMaxTime);

        if (playerRb == null) yield break;

        playerRb.linearDamping = 0.9f;
        slowEffect = null;
    }

    private IEnumerator PlayOnce()
    {
        spriteRenderer.enabled = true;
        animator.Rebind();
        animator.Update(0f);
        AudioManager.Instance.PlaySound(electricGunClip);
        animator.Play("electricGunAnimation", 0, 0f);
        yield return new WaitForSeconds(0.767f);
        if (spriteRenderer == null) yield break;
        spriteRenderer.enabled = false;
    }

     private void OnDisable()
    {
        if (player.Instance != null)
        {
            Rigidbody2D playerRb = player.Instance.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.linearDamping = 0.9f;
            }
        }
    }


}
