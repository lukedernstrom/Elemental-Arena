using UnityEngine;

public class BlueSlime : MonoBehaviour
{
    [SerializeField] private AudioClip walkClip;
    private AudioSource audioSource;
    public float speed = 2f;            
    public float movementDistance = 3f;   

    private Vector3 startPosition;
    private bool movingRight = true;

    void Start()
    {
        startPosition = transform.position;

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = walkClip;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.volume = 0.05f; //volume control
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
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (transform.position.x >= startPosition.x + movementDistance)
            {
                movingRight = false;
                Flip();
            }
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
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
