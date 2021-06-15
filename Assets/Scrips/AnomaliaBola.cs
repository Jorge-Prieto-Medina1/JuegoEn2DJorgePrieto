using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomaliaBola : MonoBehaviour
{
    public Animator animator;
    public int salud;
    public float velocidad;
    public bool muerto;
    private bool giradoDerecha;
    public Transform Objetivo;
    public Transform bola;

    void Start()
    {
        salud = 50;
        muerto = false;
        Objetivo = GameObject.FindWithTag("Player").GetComponent<Transform>();
        giradoDerecha = true;
    }


    void Update()
    {
        if (Vector3.Distance(transform.position, Objetivo.position) > 1.5f && !muerto)
        {
            transform.position = Vector3.MoveTowards(transform.position, Objetivo.position, velocidad * Time.deltaTime);
            if (((Objetivo.position.x - bola.position.x) < 0f) && !giradoDerecha && !muerto)
            {
                girar();
            }
            else
            if (((Objetivo.position.x - bola.position.x) > 0f) && giradoDerecha && !muerto)
            {
                girar();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D _col)
    {
        if (_col.gameObject.tag == "Player")
        {
            morir();
        }

    }

    public void morir()
    {
        muerto = true;
        animator.SetBool("Muerta", true);
        Destroy(gameObject, 1f);
    }

    public void girar()
    {
        giradoDerecha = !giradoDerecha;
        transform.Rotate(0f, 180f, 0f);
    }

    public void RecibirImpacto(int daño)
    {
        salud -= daño;

        if (salud <= 0)
        {
            morir();
        }

    }
}
