
using UnityEngine;
using UnityEngine.InputSystem;

public class playerShooter : MonoBehaviour
{
    // if the space bar is pressed then we can fire alr.
    [SerializeField] GameObject bullet;
    [SerializeField] AudioClip bulletFiringClip;
    private float bulletCooldown = 0f;
    private float maxBulletCooldown = 0.2f;

    private bool isFiring;
    public void SetFiring(bool value) { isFiring = value; }
    private void Update()
    {
        bulletCooldown += Time.deltaTime;
        Vector3 bulletRightOffset = transform.right * 0.21f;
        Vector3 bulletUpOffset = transform.up * 1.6f;
        bool fireInput = Keyboard.current.spaceKey.isPressed || isFiring;
        if (fireInput)
        {
            if (bulletCooldown >= maxBulletCooldown)
            {
                GameObject b = Instantiate(bullet, transform.position + bulletRightOffset + bulletUpOffset, transform.rotation);
                AudioManager.Instance.PlaySound(bulletFiringClip);
                b.GetComponent<Bullet>().SetOwner(gameObject);
                bulletCooldown = 0f;

            }
        }
    }
}
