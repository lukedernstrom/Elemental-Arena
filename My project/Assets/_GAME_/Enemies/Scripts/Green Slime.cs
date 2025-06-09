using UnityEngine;

public class GreenSlime : MonoBehaviour
{
    [SerializeField] private AudioClip walkClip;
    private AudioSource audioSource;
    public float speed = 2f;            
    public float movementDistance = 3f;   

    private Vector3 startPosition;
    private bool movingRight = false;

    void Start()
    {
        Flip(); // start facing left to go opposite direction than the blue slime
        startPosition = transform.position;

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = walkClip;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.volume = 1f; //volume control
        audioSource.Play();
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (movingRight)
        {
            transform.Translate((Vector2.right + Vector2.down) * speed * Time.deltaTime);
            if (transform.position.x >= startPosition.x + movementDistance)
            {
                movingRight = false;
                Flip();
            }
        }
        else
        {
            transform.Translate((Vector2.left + Vector2.up) * speed * Time.deltaTime);
            if (transform.position.x <= startPosition.x - movementDistance)
            {
                movingRight = true;
                Flip();
            }
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
