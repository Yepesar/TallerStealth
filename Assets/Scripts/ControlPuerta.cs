using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPuerta : MonoBehaviour {

    [SerializeField]
    private Boton boton00;
    [SerializeField]
    private Boton boton01;
    [SerializeField]
    private Boton boton02;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private AudioClip sonido_puerta;

    private AudioSource reproductor;
    private int cont = 0;

    
    // Use this for initialization
    void Start ()
    {
        reproductor = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(boton00.Activado && boton01.Activado && boton02.Activado)
        {
            AbrirPuerta();
        }
	}

    private void AbrirPuerta()
    {
        animator.SetInteger("Estado", 1);
        cont++;

        if(cont == 1)
        {
            reproductor.clip = sonido_puerta;
            reproductor.Play();
        }
    }
}
