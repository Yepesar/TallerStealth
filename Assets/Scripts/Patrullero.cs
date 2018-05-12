using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Patrullero :  MonoBehaviour  {

    [SerializeField]
    private GameObject ruta;
    [SerializeField]
    private float radio_vision;
    [Range(0, 360)]
    [SerializeField]
    private float angulo_vision;
    [SerializeField]
    private GameObject jugador;
    [SerializeField]
    private int tiempo_persecucion = 0;
    [SerializeField]
    private float velocidad_persecucion = 0;

   
    public LayerMask objetivoMask; 
    public LayerMask obstaculoMask;
    public List<Transform> objetivosVisibles = new List<Transform>();

   
    private float vel_guardado = 0;
    private bool perseguir;
    private NavMeshAgent agente;
    private int indice = 0;
    private GameObject wp;
    private bool adelante = false;
    private int contador_persecucion = 0;

    

    IEnumerator EncontrarObjetivosDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            EncontrarObjetivo();
        }
    }

    public float Radio_vision
    {
        get
        {
            return radio_vision;
        }

        set
        {
            radio_vision = value;
        }
    }

    public float Angulo_vision
    {
        get
        {
            return angulo_vision;
        }

        set
        {
            angulo_vision = value;
        }
    }

    public bool Perseguir
    {
        get
        {
            return perseguir;
        }

        set
        {
            perseguir = value;
        }
    }

    // Use this for initialization
    void Start ()
    {
        agente = GetComponent<NavMeshAgent>();
        wp = ruta.transform.GetChild(indice).gameObject;
        perseguir = false;
        StartCoroutine("EncontrarObjetivosDelay", 0.2f);
        vel_guardado = agente.speed;

       
       
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Movimiento

        if (indice == 0 && !Perseguir)
        {
            adelante = true;
        }

       if(adelante && !Perseguir)
        {
            Moverse();
        }

       if(indice == ruta.transform.childCount - 1)
        {
            adelante = false;
        }

       if(!adelante)
        {
            Devolverse();
        }

       //Persecucion

        if(Perseguir)
        {
            wp = jugador;
            agente.destination = wp.transform.position;
            contador_persecucion++;
            agente.speed = velocidad_persecucion;
            Debug.Log("Persiguiendo al jugador");

            if (contador_persecucion == tiempo_persecucion)
            {
                Debug.Log("He dejado de perseguir al jugador");
                Perseguir = false;
                contador_persecucion = 0;
                wp = ruta.transform.GetChild(indice).gameObject;
                agente.speed = vel_guardado;
            }
        }

        
       
    }

    private void Moverse()
    {
        if (Vector3.Distance(transform.position,wp.transform.position) <= 1)
        {
            indice += 1;
            wp = ruta.transform.GetChild(indice).gameObject;
        }

        agente.destination = wp.transform.position;     
    }

    private void Devolverse()
    {
        if (Vector3.Distance(transform.position, wp.transform.position) <= 1)
        {
            if (indice != 0)
            {
                indice -= 1;
                wp = ruta.transform.GetChild(indice).gameObject;
            }
        }

        agente.destination = wp.transform.position;
    }

    public Vector3 DireccionAngulo(float angulo,bool anguloGlobal)
    {

        if(!anguloGlobal)
        {
            angulo += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angulo * Mathf.Deg2Rad), 0, Mathf.Cos(angulo * Mathf.Deg2Rad));
    }

    private void EncontrarObjetivo()
    {
        objetivosVisibles.Clear();

        Collider[] cuerpos = Physics.OverlapSphere(transform.position, Radio_vision, objetivoMask);

        for (int i = 0; i < cuerpos.Length; i++)
        {
            Transform objetivo = cuerpos[i].transform;
            Vector3 direccion = (objetivo.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward,direccion) < Angulo_vision/2)
            {
                float distancia = Vector3.Distance(transform.position, objetivo.position);

                if (!Physics.Raycast(transform.position, direccion, distancia, obstaculoMask))
                {
                    objetivosVisibles.Add(objetivo);
                    Perseguir = true;
                    
                }            
            }
        }
    }

   
   
}
