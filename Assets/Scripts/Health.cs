
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private GameObject damageDelear;
    private float healthRegenartionMaxTimer = 2f;
    private float healthRegenartionTimer = 0f;
    public int currentHealth { get; private set; }


    private void Awake()
    {
        currentHealth = maxHealth;

    }

    private void OnEnable()
    {
        EventBus.Suscribe(EventBus.events.OnBossRevival, On_BossRevival);
    }

    private void On_BossRevival(GameObject boss)
    {
        if (boss != null)
        {

            currentHealth = maxHealth;
        }


    }

    private void Update()
    {
        healthRegenartionTimer += Time.deltaTime;

        if (healthRegenartionTimer >= healthRegenartionMaxTimer)
        {
            RegenrateHealth();
            healthRegenartionTimer = 0;

        }

    }


    public void TakeDamage(int Damage, GameObject damageDelear)
    {
        this.damageDelear = damageDelear;
        if (currentHealth <= 0)
        {
            return;
        }
        currentHealth = currentHealth - Damage;
        if (currentHealth <= 0)
        {
            EventBus.InvokeEvent(EventBus.events.OnDeath, gameObject);
            EventBus.InvokeEvent(EventBus.events.OnBossDeath, gameObject);
            bool victimIsEnemy = gameObject.TryGetComponent<Enemy>(out _);
            bool victimIsBoss = gameObject.TryGetComponent<boss>(out _);
            bool killedByRocket = damageDelear != null && damageDelear.TryGetComponent<rocket>(out _);
            if (victimIsEnemy)
            {
                EventBus.InvokeEvent(EventBus.events.OnEnemyDeath, gameObject);

            }


            if (killedByRocket && victimIsEnemy)
            {
                EventBus.InvokeEvent(EventBus.events.FriendlyFireKill, gameObject);
            }
            if (victimIsBoss)
            {

                EventBus.InvokeEvent(EventBus.events.OnBossDeath);
            }

        }
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    private void RegenrateHealth()
    {
        if (player.Instance.gameObject == null)
        {
            return;
        }
        if (gameObject == player.Instance.gameObject)
        {
            if (currentHealth <= maxHealth)
            {
                currentHealth = currentHealth + 10;
            }
        }
    }
}
