
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private Health health;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioClip PlayerDeathSound;
    
     
    private float deathAnimationDuration = 0.433f;
    
    
    private void Awake()
    {
        health = GetComponent<Health>();
    }

    private void Start()
    {
        
    }
    private void OnEnable()
    {
        EventBus.Suscribe(EventBus.events.OnDeath,On_death);
        
    }

    private void OnDisable()
    {
        EventBus.UnSuscribe(EventBus.events.OnDeath,On_death);
    }



    private void On_death(GameObject sender)
    {
        if(sender == gameObject)
        { 
            AudioManager.Instance.PlaySound(PlayerDeathSound);
            StartCoroutine(DeathSequence());
        }
    }

    private IEnumerator DeathSequence()
    {
        animator.Rebind();
        animator.Update(0f);
        animator.Play("blastAnimation",0,0f);
        
        yield return new WaitForSeconds(deathAnimationDuration);
        gameObject.SetActive(false);
    }

}
