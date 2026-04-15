

using System;
using UnityEngine;
using UnityEngine.UI;

public class BossHeathBarUI : MonoBehaviour
{
    [SerializeField] private Image healthBarFill;
    [SerializeField] private Health bossHealth;
    [SerializeField] private Image bossHealthBarBackground;

    private void Awake()
    {
        EventBus.Suscribe(EventBus.events.OnBossSpawned,On_BossSpawned);
        EventBus.Suscribe(EventBus.events.OnBossDeath,On_BossDeath);
    }
    private void Start()
    {
        
        
        bossHealthBarBackground.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        EventBus.UnSuscribe(EventBus.events.OnBossSpawned,On_BossSpawned);
        EventBus.UnSuscribe(EventBus.events.OnBossDeath,On_BossDeath);
    }

    private void On_BossDeath()
    {
        bossHealthBarBackground.gameObject.SetActive(false);
    }

    private void On_BossSpawned()
    {
        
        bossHealthBarBackground.gameObject.SetActive(true);
    }

    private void Update()
    {
        float fill = (float)bossHealth.currentHealth / (float)bossHealth.GetMaxHealth();
        healthBarFill.fillAmount = fill;
    }
}
