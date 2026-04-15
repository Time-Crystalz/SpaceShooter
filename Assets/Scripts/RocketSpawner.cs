using System;

using UnityEngine;

public class RocketSpawner : MonoBehaviour
{
    

    [SerializeField] private GameObject rocketPrefab; 
    
    [SerializeField] AudioClip rocketFallingClip;
    private bool CanFire = false;
    private Vector3 spawnPoint = new Vector3(0.45f, 4.06f, 0f);

    private void Start()
    {
        bossAttacks.FireRocket += SpawnRocket;
        EventBus.Suscribe(EventBus.events.OnBossArrival,On_BossArrival);
        EventBus.Suscribe(EventBus.events.OnBossDeath,On_BossDeath);
        
    }

    private void On_BossDeath()
    {
       CanFire = false;
    }

    private void On_BossArrival()
    {
        CanFire = true;
    }

    private void SpawnRocket(object sender, EventArgs e)
    {
        if (CanFire)
        {
            
        Instantiate(rocketPrefab,spawnPoint,Quaternion.identity);
        AudioManager.Instance.PlaySound(rocketFallingClip);
        }
    }

    private void OnDestroy()
    {
        bossAttacks.FireRocket -= SpawnRocket; 
        EventBus.UnSuscribe(EventBus.events.OnBossArrival,On_BossArrival);
        EventBus.UnSuscribe(EventBus.events.OnBossDeath,On_BossDeath);
    }

    

}
