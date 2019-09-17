using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [Header("Waypoints")]
    private Waypoint[] waypoints;

    private int indexAtual = -1;
    private Waypoint waypointAnterior;
    internal Waypoint waypointPosterior;

    private void Start()
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

        try
        {
            id = int.Parse(nome) - 1;
        }
        catch (Exception)
        {
            Debug.LogError("Algum Erro ocorreu. Certifique-se de que o waypoint possui um nome padrão correto waypoint(numero)");
        }

        return id;
    }

    private void AtualizarWaypoints()
    {
        waypoints = FindObjectsOfType<Waypoint>();
        waypoints = waypoints.OrderBy(objeto => PegarIdWaypoint(objeto.name)).ToArray();
    }

    private void LinkarWaypoints()
    {
        int indexAnterior = indexAtual - 1;
        int indexPosterior = indexAtual + 1;

        DefinirWaypoint(ref waypointAnterior, indexAnterior);
        DefinirWaypoint(ref waypointPosterior, indexPosterior);
    }

    private void DefinirWaypoint(ref Waypoint waypoint, int index)
    {
        if (index < 0)
        {
            index = waypoints.Length - 1;
        }
        else if (index == waypoints.Length)
        {
            index = 0;
        }

        waypoint = waypoints[index];
    }

    private void OnDrawGizmos()
    {
        CarregarSistemaWaypoint();
        if (waypointPosterior != null)
        {
            Gizmos.color = new Color(0.1f, 10.0f, 0.0f);
            Gizmos.DrawLine(transform.position, waypointPosterior.transform.position);
        }
    }
}