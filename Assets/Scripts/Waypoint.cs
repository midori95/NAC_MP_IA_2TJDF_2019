using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    [Header("Waypoints")]
    private Waypoint[] waypoints;

    private int indexAtual = -1;
    private Waypoint waypointAnterior;
    internal Waypoint waypointPosterior;

    void Start()
    {
        CarregarSistemaWaypoint();
    }

    private void CarregarSistemaWaypoint()
    {
        AtualizarWaypointAtual();
        AtualizarWaypoints();
        LinkarWaypoints();
    }
    private void AtualizarWaypointAtual()
    {
        indexAtual = PegarIdWaypoint(gameObject.name);
    }
    private int PegarIdWaypoint(string nome)
    {
        nome = nome.Replace("Waypoint (", "");
        nome = nome.Replace(")", "");

        int id = -1;

        id = int.Parse(nome);
        return id;
    }

    private void AtualizarWaypoints()
    {
        waypoints = FindObjectsOfType<Waypoint>();
        //waypoints = waypoints.OrderBy(objeto => PegarIdWaypoint(objeto, name)).toArray();
    }

    private void LinkarWaypoints()
    {
    }

   

    

    
}
