using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ControlAlarma : MonoBehaviour
{
    [SerializeField]
    private GameObject patrulleros_grupo;
    [SerializeField]
    private int duracion;
    [SerializeField]
    private Text texto_alarma;
    [SerializeField]
    private AudioClip sonido_alarma;

    
    private Patrullero [] patrulleros;
    private bool alarma;
    private int contador = 0;
    private AudioSource reproductor;

    public bool Alarma
    {
        get
        {
            return alarma;
        }

        set
        {
            alarma = value;
        }
    }

    private void Start()
    {
        patrulleros = new Patrullero[patrulleros_grupo.transform.childCount];
        AsignarScripts();
        texto_alarma.text = "Alarma: " + "DESACTIVADA";
        reproductor = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(Alarma)
        {
            contador++;

            if (contador == 1)
            {
                reproductor.clip = sonido_alarma;
                reproductor.Play();
            }

            for (int i = 0; i < patrulleros.Length; i++)
            {
                patrulleros[i].Perseguir = true;
            }

            texto_alarma.text = "Alarma: " + "ACTIVADA";

            if (contador == duracion)
            {
                contador = 0;
                for (int i = 0; i < patrulleros.Length; i++)
                {
                    patrulleros[i].Perseguir = false;
                }
                Alarma = false;
                texto_alarma.text = "Alarma: " + "DESACTIVADA";

            }
        }
    }

    private void AsignarScripts()
    {
        for (int i = 0; i < patrulleros.Length; i++)
        {
            patrulleros[i] = patrulleros_grupo.transform.GetChild(i).gameObject.GetComponent<Patrullero>();
        }
    }


}

