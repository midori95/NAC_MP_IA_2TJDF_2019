using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPorco : MonoBehaviour
{
    public GameObject porco;
    public GameObject player;
    private RaycastHit hit;
    public float rangeDeSpawn;
    public float minX, maxX;
    public float minZ, maxZ;
    private float tempo;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        porco.GetComponent<IAScript>().ia3 = true;
        porco.GetComponent<IAScript>().ia1 = false;
        porco.GetComponent<IAScript>().ia2 = false;
        porco.GetComponent<IAScript>().player = player.transform;
    }

    private void Update()
    {
        var direction = player.transform.position - transform.position;
        if (Physics.Raycast(transform.position, direction, out hit))
        {
            var canSeePlayer = hit.transform && hit.transform.CompareTag("Player");
            Debug.DrawRay(transform.position, direction, canSeePlayer ? Color.green : Color.red);

            if (canSeePlayer && Vector3.Distance(transform.position, player.transform.position) < 15)
            {
                tempo -= Time.deltaTime;
                if (tempo <= 0)
                {
                    Instantiate(porco, new Vector3(Random.Range(-minX + transform.position.x, maxX + transform.position.x),
                        porco.transform.position.y, Random.Range(-minZ + transform.position.z, maxZ + +transform.position.z)),
                        transform.rotation);

                    tempo = 8f;
                }
            }
        }
    }
}