using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public PlayerHealthData healthData;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public GameObject heartPrefab;
    public Transform heartContainer;

    private Image[] hearts;

    private void Start()
    {
        CreateHearts();
        UpdateHearts();
    }

    private void Update()
    {
        UpdateHearts();
    }

    public void CreateHearts()
    {
        // Clear old hearts
        foreach (Transform child in heartContainer)
        {
            Destroy(child.gameObject);
        }

        // Initialize new hearts
        hearts = new Image[healthData.maxHealth];

        for (int i = 0; i < healthData.maxHealth; i++)
        {
            GameObject heart = Instantiate(heartPrefab, heartContainer);
            Image heartImage = heart.GetComponent<Image>();
            hearts[i] = heartImage;
        }
    }

    public void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].sprite = i < healthData.currentHealth ? fullHeart : emptyHeart;
        }
    }
}
