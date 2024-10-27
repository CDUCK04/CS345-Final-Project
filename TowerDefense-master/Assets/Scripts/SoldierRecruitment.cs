using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class SoldierRecruitment : MonoBehaviour
{
    bool inTheStore = false;
    GameObject[] soldiers;
    int solderCount;
    public GameObject simpleSoldier,superiorSoldier,advancedSoldier;
    public Transform spawnPoint;
    public GameObject recruitmentPanel;
    public bool panelActive = false;
    GameObject player;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }
    public void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            inTheStore = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inTheStore = false;
        }
    }

    public void Update()
    {
        soldiers = GameObject.FindGameObjectsWithTag("soldier");
        solderCount = soldiers.Length;
        if (inTheStore)
        {
            if (Input.GetKeyDown(KeyCode.E) && !panelActive)
            {
                recruitmentPanel.SetActive(true);
                panelActive = true;
                Time.timeScale = 0f;
            }
        }
    }

    public void RecruitSimple()
    {
        if(solderCount <= 6 && player.GetComponent<CoinCounter>().coinAmount >= 10)
        {
            Instantiate(simpleSoldier, spawnPoint.position, Quaternion.identity);
            player.GetComponent<CoinCounter>().coinAmount -= 10;
        }
    }

   public  void RecruitSuperior()
    {
        if (solderCount <= 6 && player.GetComponent<CoinCounter>().coinAmount >= 20)
        {
            Instantiate(superiorSoldier, spawnPoint.position, Quaternion.identity);
            player.GetComponent<CoinCounter>().coinAmount -= 20;
        }
    }
    public void RecruitAdvnaced()
    {
        if (solderCount < 6 && player.GetComponent<CoinCounter>().coinAmount >= 30)
        {
            Instantiate(advancedSoldier, spawnPoint.position, Quaternion.identity);
            player.GetComponent<CoinCounter>().coinAmount -= 30;
        }
    }
    public void ClosePanel()
    {
        recruitmentPanel.SetActive(false);
        panelActive = false;
        Time.timeScale = 1f;
    }
}
