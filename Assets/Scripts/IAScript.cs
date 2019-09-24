using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using ArdJoystick;

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

    public Transform player;
    private RaycastHit hit;
    private int vidaPorco = 3;
    private bool dis;
    private bool ra;
    public GameObject arma;

    public Animator anControl;

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

    private float campoOriginal;

    private float distanciaJogador;

    [Header("Procurar")]
    public float tempoProcurar = 4f;

    private float tempoProcurando = 0f;

    private GameObject[] targets;

    public bool ia1;
    public bool ia2;
    public bool ia3;

    public ArdController ardController;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        //aiCharacterControl = GetComponent<AICharacterControl>();
    }

    private void Start()
    {
        waypintAtual = waypint1;

        Esperar();

        targets = GameObject.FindGameObjectsWithTag("Waypoint");
        campoOriginal = campoVisao;
    }

    private void Update()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        ChecarEstados();
        if (vidaPorco <= 0)
        {
            Destroy(gameObject);
        }

        if (Input.GetKey(KeyCode.LeftShift) && !PossuiVisaoJogador())
        {
            campoVisao = campoVisao * 0.8f;
        }
        else
        {
            campoVisao = campoOriginal;
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
                    anControl.SetBool("anda", true);
                }
                else
                {
                    if (ia3)
                    {
                        alvo = player;
                    }
                    else
                    {
                        alvo = transform;
                    }
                }
                break;

            case Estados.PATRULHAR:
                if (PertoWypointAtual())
                {
                    Esperar();
                    anControl.SetBool("anda", false);
                    AlternarWaypoint();
                }
                else
                {
                    if (ia3)
                    {
                        alvo = player;
                    }
                    else
                    {
                        alvo = waypintAtual;
                    }
                }

                break;

            case Estados.PERSEGUIR:

                if (!PossuiVisaoJogador())
                {
                    arma.SetActive(false);
                    Procurar();
                    anControl.SetBool("anda", true);
                    navMeshAgent.stoppingDistance = 0.5f;
                }
                else
                {
                    if (ia1)
                    {
                        //ia que atira parada
                        arma.SetActive(true);
                        transform.LookAt(player);
                        navMeshAgent.stoppingDistance = 15f;
                    }
                    else if (ia2)
                    {
                        //ia que persegue
                        transform.LookAt(player);
                        alvo = player;
                        navMeshAgent.stoppingDistance = 0f;
                    }
                    //navMeshAgent.stoppingDistance = 15f;
                }
                break;

            case Estados.PROCURAR:

                if (ProcurouTemposuficinete())
                {
                    Esperar();
                    anControl.SetBool("anda", false);
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
        //navMeshAgent.speed = 1f;
    }

    private bool PertoWypointAtual()
    {
        distanciaWaypointAtual = Vector3.Distance(transform.position, waypintAtual.position);
        return distanciaWaypointAtual <= distanciaMinimaWaypoint;
    }

    private void AlternarWaypoint()
    {
        Transform possivelTarget = null;
        GameObject possivelGameO = null;

        foreach (GameObject waypoint in targets)
        {
            float checarDistancia = Vector3.Distance(waypoint.transform.position, transform.position);

            if (possivelTarget == null || checarDistancia < Vector3.Distance(possivelTarget.position, transform.position))
            {
                possivelTarget = waypoint.transform;
                possivelGameO = waypoint;
                waypint1 = possivelGameO.GetComponent<Waypoint>().waypointPosterior.transform;
                waypint2 = possivelGameO.GetComponent<Waypoint>().waypointAnterior.transform;
            }
        }

        /*if (possivelTarget != null)
        {
            waypintAtual = waypint1;
        }*/

        waypintAtual = waypint1;
        //waypintAtual = (waypintAtual == waypint1) ? waypint2 : waypint1;
    }

    #endregion PATRULHAR

    #region PERSEGUIR

    private void Perseguir()
    {
        estadoAtual = Estados.PERSEGUIR;
        // navMeshAgent.speed = 1f;
    }

    private bool PossuiVisaoJogador()
    {
        var direction = player.transform.position - transform.position;

        if (Physics.Raycast(transform.position, direction, out hit))
        {
            var canSeePlayer = hit.transform && hit.transform.CompareTag("Player") && dis && ra;

            Debug.DrawRay(transform.position, direction, canSeePlayer ? Color.green : Color.red);
            //Debug.Log("Did Hit");
            if (hit.collider.gameObject.name == "Player")
            {
                ra = true;
            }
            else
            {
                ra = false;
            }
        }
        distanciaJogador = Vector3.Distance(transform.position, player.position);

        if (distanciaJogador <= campoVisao)
        {
            dis = true;
        }
        else
        {
            dis = false;
        }

        if (dis && ra)
        {
            return true;
        }
        else
        {
            return false;
        }
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

    #endregion PROCURAR

    private void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Player" && Input.GetKeyDown(KeyCode.Space))
        // || ardController.GetKeyDown(ArdKeyCode.BUTTON_X)
        {
            vidaPorco--;
        }

        if (collider.tag == "Player" && PossuiVisaoJogador() && ia3)
        {
            Destroy(gameObject);
        }
    }
}