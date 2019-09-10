using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerScript : MonoBehaviour
{
    public bool moveClick;
    public NavMeshAgent playerNavMesh;

    #region BLENDTREE

    public Animator an;
    public float velocidadeAtual;
    public float velocidadeRotacao = 130f;
    public float velocidadeMaxima = 3f;
    public float aceleracaoInicial = 0.2f;
    public float aceleracao = 0.01f;
    public float desaceleracao = 0.07f;

    #endregion BLENDTREE

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
        float h = Input.GetAxisRaw("Horizontal");
        Vector3 rotacao = Vector3.up * velocidadeRotacao * Time.deltaTime * h;

        float v = Input.GetAxisRaw("Vertical");

        //if (velocidadeAtual > 0)
        //{
        transform.Rotate(rotacao);
        // }

        velocidadeAtual = Mathf.Clamp(velocidadeAtual, 0, velocidadeMaxima);

        if (v > 0 && velocidadeAtual < velocidadeMaxima)
        {
            velocidadeAtual += velocidadeAtual == 0f ? aceleracaoInicial : aceleracao;
        }
        else if (v == 0 && velocidadeAtual > 0)
        {
            velocidadeAtual -= desaceleracao;
        }

        transform.Translate(velocidadeAtual * Time.deltaTime * Vector3.forward);

        float valorAnimacao = Mathf.Clamp(velocidadeAtual / velocidadeMaxima, 0f, 1f);
        an.SetFloat("Speed", valorAnimacao);
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