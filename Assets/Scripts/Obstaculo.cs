using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstaculo : MonoBehaviour
{
    public Vector3 sentidoMovimento = Vector3.left;
    public float velocidade = 20f;
    public float delayInverterMovimento = 1f;
    private int direcao = 1;
    //Sentido do Movimento --> NavMeshObstacle: Z = -1;

    private void Awake()
    {
        InvokeRepeating(nameof(InverterDirecao), delayInverterMovimento, delayInverterMovimento);
    }

    private void Start()
    {
    }

    private void Update()
    {
        transform.Translate(sentidoMovimento * direcao * velocidade * Time.deltaTime, Space.Self);
    }

    private void InverterDirecao()
    {
        direcao *= -1;
    }
}
