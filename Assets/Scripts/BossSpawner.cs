using UnityEngine;
using System;


public class BossSpawner : MonoBehaviour
{
    [SerializeField] private int killThreshold = 5;
    [SerializeField] boss boss;
    private int Kills;
    
    private bool hasAppeared = false;
    

    private void OnEnable()
    {
        EventBus.Suscribe(EventBus.events.OnEnemyDeath, OnEnemyDeath);
        EventBus.Suscribe(EventBus.events.OnBossDeath,On_BossDeath);
    }

    private void On_BossDeath()
    {
        hasAppeared = false;
        Kills = 0;
    }

    private void OnDisable()
    {
        EventBus.UnSuscribe(EventBus.events.OnEnemyDeath,OnEnemyDeath);
    }

    private void OnEnemyDeath(GameObject sender)
    {
        Kills++;
        if (hasAppeared)
        {
            return;
        }

        // float random = Random.value;
        // if (random > 0.5)
        // {
            if (Kills >= killThreshold)
            {
                hasAppeared = true;
                EventBus.InvokeEvent(EventBus.events.OnBossSpawned);
                boss.BossAppear();
                EventBus.InvokeEvent(EventBus.events.OnBossRevival,boss.gameObject);
            }

        // }
    }

    

    
}
