using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image healthBarFill;
    [SerializeField] private Health playerHealth;

    private void Update()
    {
        float fill = (float)playerHealth.currentHealth / (float)playerHealth.GetMaxHealth();
        healthBarFill.fillAmount = fill;
    }
}