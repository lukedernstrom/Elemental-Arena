using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletMoveSpeed;
    [SerializeField] private float bulletRange = 1f;
    [SerializeField] private float burstTime;
    [SerializeField] private int burstCount;
    [SerializeField] private float restTime = 1f;
    public Bullet bulletScript;
    [SerializeField] private AudioSource shooterSound; // shooting sound effect
    private bool isShooting = false;
    private int health = 5;
    [SerializeField] public GameObject deathParticles;
    [SerializeField] private float shootVolume = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void Attack()
    {
        if (!isShooting)
        {
            StartCoroutine(ShootRoutine());
        }
        
    }

    private IEnumerator ShootRoutine()
    {
        isShooting = true;
        
        for (int i = 0; i < burstCount; i++)
        {
            shooterSound.volume = shootVolume;
            shooterSound.Play();
            // GameObject.FindGameObjectWithTag("Player").transform.position;
            Vector2 targetDirection = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;

            GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            newBullet.transform.right = targetDirection;

            if (newBullet.TryGetComponent(out Bullet bullet))
            {
                bullet.UpdateMoveSpeed(bulletMoveSpeed);
                bullet.UpdateProjectileRange(bulletRange);
            }

            yield return new WaitForSeconds(burstTime);
        }

        yield return new WaitForSeconds(restTime);
        isShooting = false;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Collision");
        if (col.gameObject.tag.Equals("PlayerBullet") == true)
        {
            bulletScript = col.gameObject.GetComponent<Bullet>();
            bulletScript.EndBullet();
            health -= 1;
            if (health <= 0)
            {
                Instantiate(deathParticles, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            // Bullet other = col.gameObject.Bullet;
                // Bullet other = (Bullet) bulletPrefab.GetComponent(typeof(Bullet));
                // other.EndBullet();
                // bulletPrefab.EndBullet();
            }
    }
}
