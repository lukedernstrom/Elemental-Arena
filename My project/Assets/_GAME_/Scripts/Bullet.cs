using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float projectileRange = 1f; //How long until bullet despawns
    [SerializeField] private float moveSpeed = 1f;

    private ParticleSystem particleSystem;
    private SpriteRenderer sr;
    public bool once = true;
    private Vector3 spawnPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnPoint = transform.position;
        particleSystem = GetComponent<ParticleSystem>();
        sr = GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        MoveProjectile();
        DetectFireDistance();
    }

    public void UpdateProjectileRange(float projectileRange)
    {
        this.projectileRange = projectileRange;
    }

    public void UpdateMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    private void MoveProjectile()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
    }

    private void DetectFireDistance()
    {
        if ((Vector3.Distance(transform.position, spawnPoint) > projectileRange) && once)
        {
            once = false;
            EndBullet();
            // var em = particleSystem.emission;
            // var dur = particleSystem.duration;
            // em.enabled = true;
            // particleSystem.Play();
            // // Destroy(gameObject);
            // Debug.Log("Particle");
            // Destroy(sr);
            // Invoke(nameof(DestroyObj), dur);
        }
    }

    public void EndBullet()
    {
        var em = particleSystem.emission;
        var dur = particleSystem.duration;
        em.enabled = true;
        particleSystem.Play();
        // Destroy(gameObject);
        Debug.Log("Particle");
        Destroy(sr);
        Invoke(nameof(DestroyObj), dur);
    }
    void DestroyObj()
    {
        Destroy(gameObject);
    }
}

