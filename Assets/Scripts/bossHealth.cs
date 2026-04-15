using System.Collections;
using UnityEngine;

public class bossHealth : MonoBehaviour
{

    [SerializeField] private AudioClip BossDeathClip;
    [SerializeField] private Animator animator;
    private float deathAnimationDuration = 0.5f;
    


    private void Awake()
    {

    }
    void OnEnable()
    {
        EventBus.Suscribe(EventBus.events.OnBossDeath, On_BossDeath);
    }

    private void OnDisable()
    {

        EventBus.UnSuscribe(EventBus.events.OnBossDeath, On_BossDeath);
    }

    private void On_BossDeath(GameObject sender)
    {
        if (gameObject == sender)
        {
            AudioManager.Instance.PlaySound(BossDeathClip);
            StartCoroutine(DeathSequence());

        }

    }

     private IEnumerator DeathSequence()
    {
        animator.Rebind();
        animator.Update(0f);
        animator.Play("bossBlastAnimtion",0,0f);
        
        yield return new WaitForSeconds(deathAnimationDuration);
        gameObject.SetActive(false);
    }

}


