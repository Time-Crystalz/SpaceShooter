using System;
using System.Collections.Generic;
using UnityEngine;

public static class EventBus 
{
    public enum events
    {
        OnDeath,  
        OnPlayerDamaged,  
        OnEnemyDeath,
        FriendlyFireKill,
        OnBossSpawned,
        OnBossDeath,
        OnBossRevival,
        OnBossArrival,
        
    }

    public enum EnitityType
    {
        player,
        enemy,
        projectile
    }
    private static Dictionary<string,Action> listners = new Dictionary<string, Action>();
    private static Dictionary<string,Action <GameObject>> listnersWithSenders = new();
    public static void Suscribe(Enum eventnames, Action callBack)
    {
        string key = eventnames.ToString();
        if (!listners.ContainsKey(key))
        {
            listners[key] = null;
        }
        
        listners[eventnames.ToString()] += callBack;
    }

    public static void Suscribe(Enum eventnames, Action<GameObject> callBack)
    {
        string key = eventnames.ToString();
        if (!listnersWithSenders.ContainsKey(key))
        {
            listnersWithSenders[key] = null;
        }
        
        listnersWithSenders[eventnames.ToString()] += callBack;
    }
     public static void UnSuscribe(Enum eventnames, Action callBack)
    {
        string key = eventnames.ToString();
        if (listners.ContainsKey(key))
            listners[key] -= callBack;
    }
    public static void UnSuscribe(Enum eventnames, Action<GameObject> callBack)
    {
        string key = eventnames.ToString();

        if (listnersWithSenders.ContainsKey(key))
        {
            listnersWithSenders[key] -= callBack;
        }
    }


    public static void InvokeEvent(Enum eventnames)
    {
        string key = eventnames.ToString();
        if (listners.ContainsKey(key))
        {
            
            listners[key]?.Invoke();
        }
    }

    public static void InvokeEvent(Enum eventnames,GameObject sender)
    {
        string key = eventnames.ToString();
        if (listnersWithSenders.ContainsKey(key))
        {
            Debug.Log("Listners_With_Sender has activated");
            listnersWithSenders[key]?.Invoke(sender);
        }
    }
}
