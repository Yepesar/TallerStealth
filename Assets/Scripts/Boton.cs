using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boton : MonoBehaviour {

    [SerializeField]
    private bool activado = false;
    [SerializeField]
    private AudioClip sonido_boton;

    private AudioSource reproductor;
    private bool reproducir = true;

    public bool Activado
    {
        get
        {
            return activado;
        }

        set
        {
            activado = value;
        }
    }

    // Use this for initialization
    void Start ()

    {
        reproductor = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()

    {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            activado = true;

            if (reproducir)
            {
                reproductor.clip = sonido_boton;
                reproductor.Play();
                reproducir = false;
            }
            
        }
    }
}
