using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    public string coinCount;
    public int coinAmount;

    private void Update()
    {
        coinCount = coinAmount.ToString();  
    }
}
