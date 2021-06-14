using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public int salud;
    public float velocidad;
    private bool muerto;
    private bool giradoDerecha;
    public Animator animator;
    public Transform Objetivo;
    public Transform zombie;
    private bool atacando;
    public GameObject ataque;
    public GameObject posicionAtaque;

    // Start is called before the first frame update
    void Start()
    {
        salud = 100;
        muerto = false;
        Objetivo = GameObject.FindWithTag("Player").GetComponent<Transform>();
        giradoDerecha = true;
        atacando = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, Objetivo.position) > 1.5f && !muerto)
        {
            transform.position = Vector3.MoveTowards(transform.position, Objetivo.position, velocidad * Time.deltaTime);
            if (((Objetivo.position.x - zombie.position.x) < 0f) && !giradoDerecha && !muerto)
            {
                girar();
            }
            else
            if(((Objetivo.position.x - zombie.position.x) > 0f) && giradoDerecha && !muerto)
            {
                girar();
            }
        }
        if (Vector3.Distance(transform.position, Objetivo.position) < 3f && !muerto)
        {
            atacar();
            animator.SetBool("Atacar", true);
        }
        else
        {
            animator.SetBool("Atacar", false);
        }
       
    }

    public void RecibirImpacto(int daño)
    {
        salud -= daño;

        if (salud <= 0)
        {
            Morir();
        }

    }

    private void Morir()
    {
        muerto = true;
        animator.SetBool("Muerto", true);
        Invoke("Destruir", 1f);
    }

    private void Destruir()
    {
        Destroy(gameObject);
    }

    private void TerminaDeAtacar()
    {
        atacando = false;
    }

    public void atacar()
    {
        if (atacando == false)
        {
            GameObject.Instantiate(ataque, posicionAtaque.transform.position,
            posicionAtaque.transform.rotation);
            atacando = true;
            Invoke("TerminaDeAtacar", 1f);
        }
    }


    public void girar()
    {
        giradoDerecha = !giradoDerecha;
        transform.Rotate(0f, 180f, 0f);
    }

}
