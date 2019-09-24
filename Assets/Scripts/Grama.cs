using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grama : MonoBehaviour
{
    private Material material;

    private void Start()
    {
        material = GetComponent<Material>();
    }

    private void Update()
    {
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Player")
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.5f);
        }

        print(collider.tag);
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
        }
    }
}