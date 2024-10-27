using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;

    [Header("Attributes")]
    public float range = 15f;
    public float minRange = 5f;
    public float fireRate = 2f;
    private float fireCountdown = 0f;
    public float fieldOfView = 90f;
    [Header("Unity Setup")]
    public string enemyTag = "Enemy";
    public Transform partToRotate;
    public float turnSpeed = 10f;
    public GameObject bulletPrefab;
    public Transform firePoint; // Where the bullet spawns

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies) {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance && distanceToEnemy >= minRange) {
                //shortestDistance = distanceToEnemy;
                //nearestEnemy = enemy;
                Vector3 directionToEnemy = (enemy.transform.position - transform.position).normalized;
                float angleToEnemy = Vector3.Angle(transform.forward, directionToEnemy);

                if (angleToEnemy <= (fieldOfView / 2)) {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }
        }

        if (nearestEnemy != null && shortestDistance <= range) {
            target = nearestEnemy.transform;
        }
        else {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;

        // Target lock on
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        
        // Part to rotate makes the turret look at the enemy
        // Specified GameObject turns and faces the enemy
        if (partToRotate != null) {
            Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }

        if (fireCountdown <= 0) {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot() {
        GameObject bulletObject = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletObject.GetComponent<Bullet>();

        if (bullet != null) {
            bullet.Seek(target);
        }
    }

    void OnDrawGizmosSelected() {
        // Red lines are attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);

        // Yellow lines are the minimum range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, minRange);

        // Blue lines are field of view
        Vector3 forwardLeft = Quaternion.Euler(0, -fieldOfView / 2, 0) * transform.forward * range;
        Vector3 forwardRight = Quaternion.Euler(0, fieldOfView / 2, 0) * transform.forward * range;
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + forwardLeft);
        Gizmos.DrawLine(transform.position, transform.position + forwardRight);
    }
}
