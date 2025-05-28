using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletMoveSpeed;
    [SerializeField] private float burstTime;
    [SerializeField] private int burstCount;
    [SerializeField] private float restTime = 1f;

    private bool isShooting = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    
    }

    public void Attack()
    {
        Debug.Log("Attack");
        if (!isShooting)
        {
            StartCoroutine(ShootRoutine());
        }
        
    }

    private IEnumerator ShootRoutine()
    {
        Debug.Log("ShootRoutine");
        isShooting = true;

        for (int i = 0; i < burstCount; i++)
        {

            // GameObject.FindGameObjectWithTag("Player").transform.position;
            Vector2 targetDirection = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;

            GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Debug.Log("Made bullet");
            newBullet.transform.right = targetDirection;

            if (newBullet.TryGetComponent(out Bullet bullet))
            {
                bullet.UpdateMoveSpeed(bulletMoveSpeed);
            }

            yield return new WaitForSeconds(burstTime);
        }

        

        yield return new WaitForSeconds(restTime);
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }
}
