using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.UI;

public class CoinPickUp : MonoBehaviour
{
    public int coinValue;
    AudioSource audioSource;
    public AudioClip pickUp;
    public Text coinCount;
    public GameObject[] coins;
    GameObject coin;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(gameObject.tag == "Copper") { coinValue = Random.Range(1,5); }
            else if (gameObject.tag == "Silver") { coinValue = Random.Range(6, 8); }
            else if (gameObject.tag == "Gold") { coinValue = Random.Range(9, 10); }
            other.gameObject.GetComponent<CoinCounter>().coinAmount += coinValue;
            audioSource.PlayOneShot(pickUp);

            Destroy(gameObject);
        }
    }

}
