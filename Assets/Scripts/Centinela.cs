using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Centinela : MonoBehaviour {

    [SerializeField]
    private int timepo_persecucion;

    private NavMeshAgent agente;
    private bool perseguir = false;
    private int contador_persecucion = 0;
    private GameObject objetivo;
    private Vector3 pos_inicial;

	// Use this for initialization
	void Start ()
    {
        agente = GetComponent<NavMeshAgent>();
        pos_inicial = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(perseguir)
        {
            Correr();
            contador_persecucion++;

            if (contador_persecucion == timepo_persecucion)
            {
                contador_persecucion = 0;
                agente.destination = pos_inicial;
                perseguir = false;
                Debug.Log("Lo he perdido!");
            }
        }
    
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            perseguir = true;
            objetivo = other.gameObject;
            Debug.Log("Alto ahi!");
        }
    }

    private void Correr()
    {
        agente.destination = objetivo.transform.position;
    }
}
