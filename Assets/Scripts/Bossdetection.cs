using System;
using UnityEngine;

public class Bossdetection : MonoBehaviour
{
    private player playerBody;
    private Rigidbody2D rigidbody2D;
    private float attackChance = 0.3f;
    private bossAttacks bossAttacks;
    public EventHandler<OnPlayerDetectedEventArgs> OnPlayerEnters;

    public class OnPlayerDetectedEventArgs : EventArgs
    {
        public Health playerHealth;
        public Rigidbody2D playerRigidBody;
    }

    private void Awake()
    {
        bossAttacks = GetComponentInParent<bossAttacks>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Health health;
        other.TryGetComponent<player>(out playerBody);
        other.TryGetComponent<Rigidbody2D>(out rigidbody2D);

        if (playerBody)
        {
            health = playerBody.GetComponent<Health>();

            if (UnityEngine.Random.value <= attackChance)
            {
                OnPlayerEnters?.Invoke(this, new OnPlayerDetectedEventArgs()
                {
                    playerHealth = health,
                    playerRigidBody = rigidbody2D,
                });
                bossAttacks.IfPlayerEnters = true;

            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        other.TryGetComponent<player>(out playerBody);

        if (playerBody)
        {
            
            bossAttacks.IfPlayerEnters = false;
        }
    }
}
