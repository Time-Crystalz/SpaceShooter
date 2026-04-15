using UnityEngine;

public class EnemyBullets : MonoBehaviour
{
    
    private float bulletX;
    private float bulletY;
    private float speed = 17f;
    private float xBoundary = 16.97f;
    private float yBoundary = 9.97f;
    private GameObject owner;
    [SerializeField] private int  damage = 10;
    [SerializeField] private boss boss;

    private void Awake()
    {
        
    }

    public void SetOwner(GameObject owner)
    {
        this.owner = owner;
    }

    private void Update()
    {
        bulletX = transform.position.x;
        bulletY = transform.position.y;

        gameObject.transform.position += transform.up * speed * Time.deltaTime;

        DeleteOffScreenBullets();
    }

    private void DeleteOffScreenBullets()
    {
        float x = transform.position.x;
        float y = transform.position.y;

         if(x < -xBoundary )
        {
            Destroy(gameObject);
        }
       if(x > xBoundary )
        {
           Destroy(gameObject);
        }
       if(y > yBoundary )
        {
           Destroy(gameObject);
        }
       if(y < -yBoundary )
        {
            Destroy(gameObject);
        }
     
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == owner)
        {
            return;
        }   
        if (other.gameObject == boss)
        {
            return;
        }
        if(other.gameObject.TryGetComponent(out Health health))
        {
            health.TakeDamage(damage,gameObject);
            Destroy(gameObject);
        }
    }
}
