using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private float velocidad_movimiento = 0;
    [SerializeField]
    private Slider barra_vida;

    private float vida = 1;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()

    {
	
        //Movimiento

        if(Input.GetKey(KeyCode.W))
        {
            Moverse(0, -1);
        }

        if (Input.GetKey(KeyCode.S))
        {
            Moverse(0, 1);
        }

        if (Input.GetKey(KeyCode.A))
        {
            Moverse(1, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            Moverse(-1, 0);
        }

        //Vida

        barra_vida.value = vida;

        if(vida <=0)
        {
            SceneManager.LoadScene("Menu");
        }

    }

    private void Moverse(int dir_x, int dir_z)
    {
        Vector3 direccion = new Vector3(transform.position.x + dir_x, transform.position.y, transform.position.z + dir_z);
        transform.position = Vector3.MoveTowards(transform.position, direccion, velocidad_movimiento * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Guardia")
        {
            vida -= 0.05f;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Guardia")
        {
            vida -= 0.05f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Puerta")
        {
            SceneManager.LoadScene("Victoria");
        }
    }
}
