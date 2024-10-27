using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CoinDisplay : MonoBehaviour
{
    public Text coinDisplay;
    GameObject player;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        coinDisplay.text = player.GetComponent<CoinCounter>().coinCount;
    }
}
