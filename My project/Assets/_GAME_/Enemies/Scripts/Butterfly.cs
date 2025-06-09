using UnityEngine;

public class Butterfly : MonoBehaviour
{
    [SerializeField] private AudioClip idleClip;
    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = idleClip;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.volume = 0.2f;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
