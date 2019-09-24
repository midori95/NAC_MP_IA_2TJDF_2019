using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using ArdJoystick;

public class PlayerScript : MonoBehaviour
{
    public bool moveClick;
    public NavMeshAgent playerNavMesh;
    public Rigidbody rb;
    public float forcaPulo = 250f;
    public static bool agachado;
    public static int life = 10;
    public GameObject hitPorco;
    private bool podePular;
    private GameObject[] porcos;
    private int numeroP;
    public ArdController ardController;

    #region BLENDTREE

    public Animator an;

    [Header("Atributos de Velocidade")]
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
        agachado = Input.GetKey(KeyCode.LeftShift);
        if (moveClick)
        {
            SetDestination();
        }
        else
        {
            Move();
        }

        porcos = GameObject.FindGameObjectsWithTag("Porco");
        numeroP = porcos.Length;
        if (numeroP <= 0)
        {
            SceneManager.LoadScene("");
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

        // CONTROLE DE INPUTS DE VELOCIDADE DE MOVIMENTO
        if (Input.GetKey(KeyCode.LeftControl) || ardController.GetKeyDown(ArdKeyCode.BUTTON_A))
        {
            transform.Translate((velocidadeAtual * Time.deltaTime * Vector3.forward) * 1.5f);
        }
        else if (agachado)
        {
            transform.Translate((velocidadeAtual * Time.deltaTime * Vector3.forward) / 2);
        }
        else if (!agachado)
        {
            transform.Translate(velocidadeAtual * Time.deltaTime * Vector3.forward);
        }

        float valorAnimacao = Mathf.Clamp(velocidadeAtual / velocidadeMaxima, 0f, 1f);
        an.SetFloat("Speed", valorAnimacao);

        if (Input.GetKeyDown(KeyCode.J) && podePular)
        {
            rb.AddForce(Vector3.up * forcaPulo);
        }
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

    private void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Porco" && Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(hitPorco, collider.transform.position, transform.rotation);
        }

        if (collider.tag == "Chao")
        {
            podePular = true;
        }

        print(collider.tag);
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Chao")
        {
            podePular = false;
        }
    }

    /*
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
    }*/
}