using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAScript : MonoBehaviour
{
    public enum Estados
    {
        ESPERAR,
        PATRULHAR,
        PERSEGUIR,
        PROCURAR
    }

    public Estados estadoAtual;

    private Transform alvo;

    private NavMeshAgent navMeshAgent;
    //private AICharacterControl aiCharacterControl;

    private Transform player;
    private int vidaPorco = 3;

    [Header("Esperar")]
    public float tempoEsperar = 2f;

    private float tempoEsperando = 0f;

    [Header("Patrulhar")]
    public Transform waypint1;

    public Transform waypint2;
    private Transform waypintAtual;
    public float distanciaMinimaWaypoint = 1f;
    private float distanciaWaypointAtual;

    [Header("Perseguir")]
    public float campoVisao = 5f;

    private float distanciaJogador;

    [Header("Procurar")]
    public float tempoProcurar = 4f;

    private float tempoProcurando = 0f;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        //aiCharacterControl = GetComponent<AICharacterControl>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        waypintAtual = waypint1;

        Esperar();
    }

    private void Update()
    {
        ChecarEstados();
        if (vidaPorco <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void ChecarEstados()
    {
        if (estadoAtual != Estados.PERSEGUIR && PossuiVisaoJogador())
        {
            Perseguir();
            return;
        }

        switch (estadoAtual)
        {
            case Estados.ESPERAR:

                if (EsperouTemposuficinete())
                {
                    Patrulhar();
                }
                else
                {
                    alvo = transform;
                }
                break;

            case Estados.PATRULHAR:
                if (PertoWypointAtual())
                {
                    Esperar();
                    AlternarWaypoint();
                }
                else
                {
                    alvo = waypintAtual;
                }

                break;

            case Estados.PERSEGUIR:

                if (!PossuiVisaoJogador())
                {
                    Procurar();
                }
                else
                {
                    alvo = player;
                }
                break;

            case Estados.PROCURAR:

                if (ProcurouTemposuficinete())
                {
                    Esperar();
                }
                else
                {
                    alvo = null;
                }
                break;
        }
        /*
        if (aiCharacterControl)
        {
            aiCharacterControl.SetTarget(alvo);
        }*/
        if (alvo != null)
        {
            navMeshAgent.destination = alvo.position;
        }
    }

    #region ESPERAR

    private void Esperar()
    {
        estadoAtual = Estados.ESPERAR;

        tempoEsperando = Time.time;
    }

    private bool EsperouTemposuficinete()
    {
        return tempoEsperando + tempoEsperar <= Time.time;
    }

    #endregion ESPERAR

    #region PATRULHAR

    private void Patrulhar()
    {
        estadoAtual = Estados.PATRULHAR;
        navMeshAgent.speed = 0.5f;
    }

    private bool PertoWypointAtual()
    {
        distanciaWaypointAtual = Vector3.Distance(transform.position, waypintAtual.position);
        return distanciaWaypointAtual <= distanciaMinimaWaypoint;
    }

    private void AlternarWaypoint()
    {
        waypintAtual = (waypintAtual == waypint1) ? waypint2 : waypint1;
    }

    #endregion PATRULHAR

    #region PERSEGUIR

    private void Perseguir()
    {
        estadoAtual = Estados.PERSEGUIR;
        navMeshAgent.speed = 1f;
    }

    private bool PossuiVisaoJogador()
    {
        distanciaJogador = Vector3.Distance(transform.position, player.position);
        return distanciaJogador <= campoVisao;
    }

    #endregion PERSEGUIR

    #region PROCURAR

    private void Procurar()
    {
        estadoAtual = Estados.PROCURAR;

        tempoProcurando = Time.time;
    }

    private bool ProcurouTemposuficinete()
    {
        return tempoProcurando + tempoProcurar <= Time.time;
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Player" && Input.GetKeyDown(KeyCode.Space))
        {
            vidaPorco--;
        }
    }

    #endregion PROCURAR
}