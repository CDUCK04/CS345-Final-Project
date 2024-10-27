using DM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierKilling : MonoBehaviour
{
    
    public Soldiers soldier;
    public int Damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && soldier.isAttacking == true)
        {
            Enemy enemyComponent = other.GetComponent<Enemy>();
   
            enemyComponent.TakeDamage(Damage);
        }
    }
    
}
