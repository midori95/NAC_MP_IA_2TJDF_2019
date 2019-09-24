using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MudaCena : MonoBehaviour
{
    private GameObject[] porcos;
    private int numeroP;
    public string cena;

    private void Start()
    {
    }

    private void Update()
    {
        porcos = GameObject.FindGameObjectsWithTag("Porco");
        numeroP = porcos.Length;
        if (numeroP <= 0)
        {
            SceneManager.LoadScene(cena);
        }
    }
}