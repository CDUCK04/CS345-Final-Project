using DM;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Killing : MonoBehaviour
{
    //public string score;
    //public int scoreCount = 0;
    //public Text scoreText;
    public ControlManager hero;
    public Soldiers soldier;
    public int Damage;
    //private Animator animator;
    //private GameObject enemy;
    //public GameObject enemies;
    //public GameObject youWin;
    void Start()
    {
        //Time.timeScale = 1.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if( other.tag == "Enemy" && hero.isAttacking == true)
        {
            Enemy enemyComponent = other.GetComponent<Enemy>();
            //animator = other.GetComponent<Animator>();
            //Hero.GetComponent<ControlManager>().attacking = false;
            //animator.SetBool("Died", true);
            //enemy = other.gameObject;
            //StartCoroutine(WaitForAnimationToEnd("Die"));
            //scoreCount++;
            //score = "Hits: " + scoreCount.ToString();  
            //scoreText.text = score;
            enemyComponent.TakeDamage(Damage);
        }
    }
    /*private IEnumerator WaitForAnimationToEnd(string animationName)
    {
       
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName(animationName));
       
        yield return new WaitUntil(() => !animator.GetCurrentAnimatorStateInfo(0).IsName(animationName));
       
        //Destroy(enemy);
    }*/

    public void Update()
    {
        /*if (enemies.transform.childCount == 0)
        {
            youWin.SetActive(true);
            Time.timeScale = 0f;
        }*/
    }
}
