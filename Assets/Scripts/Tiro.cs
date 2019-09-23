using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiro : MonoBehaviour
{
    private Rigidbody rb;
    public float forca = 500f;
    public GameObject hitLobo;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 5f);
        //.AddForce(Vector3.forward * forca);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * 50f);
    }

    private void OnCollisionEnter(Collision c)
    {
        if (c.collider.tag == "Player")
        {
            Instantiate(hitLobo, c.transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}