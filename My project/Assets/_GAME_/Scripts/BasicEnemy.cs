using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    private bool facingRight = false;
    private float walkSpeed = 2f;
    // private Transform body;
    private Vector3 pos;
    private float minX, maxX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Starting enemy script!");
        pos = transform.position;
        minX = pos.x - 5f;
        maxX = pos.x + 5f;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        pos = transform.position;
        if (pos.x > maxX)
        {
            facingRight = false;
        }
        else if (pos.x < minX)
        {
            facingRight = true;
        }

        float direction = facingRight ? 1f : -1f;
        transform.Translate(Vector3.right * direction * walkSpeed * Time.deltaTime);
    }
}
