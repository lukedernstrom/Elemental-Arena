using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public PlayerHealthData healthData;

    private void Start()
    {
        healthData.ResetHealth();
    }

    public void TakeDamage(int amount)
    {
        healthData.TakeDamage(amount);
        FindObjectOfType<HealthUI>().UpdateHearts();
    }

    public void Heal(int amount)
    {
        healthData.Heal(amount);
        FindObjectOfType<HealthUI>().UpdateHearts();
    }
}
