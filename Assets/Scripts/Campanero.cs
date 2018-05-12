using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Campanero : MonoBehaviour  {

    [SerializeField]
    private GameObject puntos;
    [SerializeField]
    private int timpo_espera;

    private ControlAlarma alarma;
    private int numero_puntos;
    private NavMeshAgent agente;
    private int contador = 0;
    private int contador_alarma = 0;

   

	// Use this for initialization
	void Start ()
    {
        agente = GetComponent<NavMeshAgent>();
        numero_puntos = puntos.transform.childCount;
        alarma = GetComponent<ControlAlarma>();
      
	}
	
	// Update is called once per frame
	void Update ()
    {
        contador++;
        if(contador == timpo_espera)
        {
            Moverse();
            contador = 0;
        }

        
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            alarma.Alarma = true;
        }
    }

    private void Moverse()
    {
        GameObject destino = puntos.transform.GetChild(Random.Range(0, numero_puntos)).gameObject;
        agente.destination = destino.transform.position;
    }

   
}
