using UnityEngine;

public class enmeyShooter : MonoBehaviour
{
    [SerializeField] GameObject bullet;
   
    private float bulletCooldown = 0f;
    private float maxBulletCooldown = 0.2f;
    private EnemyDetection enemyDetection;
    [SerializeField]private AudioClip bulletFiringClip;
    public player playerBody;

   

    public bool isFiring = false;

    private void Awake()
    {
        enemyDetection = GetComponentInChildren<EnemyDetection>();    
    }

    private void Update()
    {
        bulletCooldown += Time.deltaTime;
        if (bulletCooldown >= maxBulletCooldown)
        {

            if (isFiring)
            {
                FireBullets();
                bulletCooldown = 0;
                

            }
        }

         FaceAtPlayer();
    }

    private void FireBullets()
    {
                Vector3 bulletRightOffset = transform.right * 0.21f;
                Vector3 bulletUpOffset = transform.up * 1.5f;
                GameObject b = Instantiate(bullet, transform.position + bulletRightOffset + bulletUpOffset, transform.rotation);  
                 AudioManager.Instance.PlaySound(bulletFiringClip);        
                b.GetComponent<EnemyBullets>().SetOwner(gameObject);
    }   

     private void  FaceAtPlayer()
    {
        if (playerBody == null)
        {
            return;
        }
        if (playerBody)
        {   
                Vector2 direction = (Vector2)(playerBody.transform.position - transform.position);
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
                transform.rotation = Quaternion.Euler(0, 0, angle); 
        }
    }




}
