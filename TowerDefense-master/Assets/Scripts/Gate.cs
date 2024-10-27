using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Gate : MonoBehaviour
{
    private bool opened= false;
    public GameObject gate;

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
           opened = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            opened = false;
        }
    }

    private void Update()
    {
        if (opened)
        {
            if (Input.GetKey(KeyCode.E))
            {
                if (gate.transform.position.y > 18.2)
                {
                    transform.Rotate(0, 0, 2);
                    gate.transform.Translate(0, 0, 0.025f);
                }
            }

            if (Input.GetKey(KeyCode.Q))
            {
                if (gate.transform.position.y < 21.75)
                {
                    transform.Rotate(0, 0, -2);
                    gate.transform.Translate(0, 0, -0.025f);
                }
            }
        }
    }

}
