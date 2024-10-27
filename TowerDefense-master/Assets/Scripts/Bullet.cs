using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float speed = 30f;
    public GameObject impactEffect;
    
    public void Seek(Transform _target) {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame) {
            HitTarget();
            Damage(target);
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget() {
        GameObject effect = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effect, 2f);
        Destroy(gameObject);
    }

    void Damage(Transform enemy) {
        if (enemy.CompareTag("Enemy")) {
            Enemy e = enemy.GetComponent<Enemy>();
            if (e != null) {
                e.TakeDamage(1);
            }
        }
    }
}
