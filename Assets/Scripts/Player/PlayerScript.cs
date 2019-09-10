using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerScript : MonoBehaviour
{
    public bool moveClick;
    public NavMeshAgent playerNavMesh;

    private void Start()
    {
    }

    private void Update()
    {
        if (moveClick)
        {
            SetDestination();
        }
        else
        {
            Move();
        }
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * Input.GetAxis("Vertical") * velocidade);

        an.SetFloat("Blend", Input.GetAxis("Vertical"));

        if (Input.GetKey(KeyCode.LeftShift))
        {
            an.speed = 2;
            velocidade = 4;
        }
        else
        {
            an.speed = 1;
            velocidade = 2;
        }

        transform.Rotate(0, Input.GetAxis("Horizontal") * velocidade, 0, Space.Self);
    }

    private void SetDestination()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                playerNavMesh.destination = hit.point;
            }
        }
    }
}