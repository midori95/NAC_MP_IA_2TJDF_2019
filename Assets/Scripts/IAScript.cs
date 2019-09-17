using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAScript : MonoBehaviour
{
    public static int vidaPorco = 3;

    private void Start()
    {
    }

    private void Update()
    {
        if (vidaPorco <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Player" && Input.GetKeyDown(KeyCode.Space))
        {
            vidaPorco--;
        }
    }
}