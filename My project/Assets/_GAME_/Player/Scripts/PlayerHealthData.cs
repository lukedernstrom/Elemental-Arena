using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class PlayerHealthData : ScriptableObject
{
    public int maxHealth = 3;
    public int currentHealth = 3;

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth = Mathf.Max(currentHealth - amount, 0);
        if (currentHealth == 0)
        {
            SceneManager.LoadScene("Title");
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }
}
