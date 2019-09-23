using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma : MonoBehaviour
{
    public GameObject tiro;
    public float tempo = 2f;

    private void Start()
    {
    }

    private void Update()
    {
        tempo -= Time.deltaTime;
        if (tempo <= 0)
        {
            Instantiate(tiro, transform.position, transform.rotation);
            tempo = 2f;
        }
    }
}