using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float projectileRange = 1f; //How long until bullet despawns
    [SerializeField] private float moveSpeed = 1f;

    private Vector3 spawnPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnPoint = transform.position;
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
        if (Vector3.Distance(transform.position, spawnPoint) > projectileRange)
        {
            Destroy(gameObject);
        }   
    }
}

