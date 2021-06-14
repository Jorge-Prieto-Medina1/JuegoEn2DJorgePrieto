using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{

    public Transform PuntoDeDisparo;
    public Transform PuntoDeDisparoArriba;
    public Transform PuntoDeDisparoAbajo;
    public GameObject prefabBala;
    public Personaje jugador;
    public Animator animator;
    public Animator rifleAnimator;
    public Animator fogonazo;
    public Animator fogonazoArriba;
    public Animator fogonazoAbajo;

    private bool disparando;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.S))
        {
            if (!jugador.Saltando)
            {
                dispararArriba();
            }
        }

        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S))
        {
            if (!jugador.Saltando)
            {
                float horizontal = 1f;
                jugador.girar(horizontal);
                disparar();
            }
                
        }

        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S))
        {
            if (!jugador.Saltando)
            {
                float horizontal = -1f;
                jugador.girar(horizontal);
                disparar();
            }
 
        }



        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftArrow))
        {
            if (!jugador.Saltando)
            {
                float horizontal = -1f;
                jugador.girar(horizontal);
                dispararAbajo();
            }

        }

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.RightArrow))
        {
            if (!jugador.Saltando)
            {
                float horizontal = 1f;
                jugador.girar(horizontal);
                dispararAbajo();
            }

        }
        if ((Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKey(KeyCode.UpArrow)) && jugador.RifleDeAsalto == true )
        {
            rifleAnimator.SetBool("Disparando", false);
            fogonazo.SetBool("DispararFogonazoRifle", false);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow)  && jugador.RifleDeAsalto == true)
        {
            rifleAnimator.SetBool("DisparandoArriba", false);
            fogonazoArriba.SetBool("DispararFogonazoRifle", false);
        }
        if ((Input.GetKeyUp(KeyCode.LeftArrow)) || (Input.GetKeyUp(KeyCode.RightArrow) || rifleAnimator.GetBool("Disparando")) && jugador.RifleDeAsalto == true)
        {
            fogonazoAbajo.SetBool("DispararFogonazoRifle", false);
        }


    }


    private void TerminaDeDisparar()
    {
        disparando = false;
    }

    void disparar()
    {
        if (disparando == false)
        {
            if(!jugador.RifleDeAsalto)
            {
                disparando = true;
                fogonazo.SetTrigger("DispararFogonazo");
                Instantiate(prefabBala, PuntoDeDisparo.position, PuntoDeDisparo.rotation);
                animator.SetTrigger("Disparar");
                Invoke("TerminaDeDisparar", 0.5f);
            }
            else
            {
                disparando = true;
                fogonazo.SetBool("DispararFogonazoRifle", true);
                Instantiate(prefabBala, PuntoDeDisparo.position, PuntoDeDisparo.rotation);
                rifleAnimator.SetBool("Disparando", true);
                Invoke("TerminaDeDisparar", 0.1f);
            }
            
        }
    }

    void dispararArriba()
    {
        if (disparando == false)
        {
            if (!jugador.RifleDeAsalto)
            {
                disparando = true;
                fogonazoArriba.SetTrigger("DispararFogonazo");
                Instantiate(prefabBala, PuntoDeDisparoArriba.position, PuntoDeDisparoArriba.rotation);
                animator.SetTrigger("DispararArriba");
                Invoke("TerminaDeDisparar", 0.5f);
            }
            else
            {
                disparando = true;
                fogonazoArriba.SetBool("DispararFogonazoRifle", true);
                Instantiate(prefabBala, PuntoDeDisparoArriba.position, PuntoDeDisparoArriba.rotation);
                rifleAnimator.SetBool("DisparandoArriba", true);
                Invoke("TerminaDeDisparar", 0.1f);
            }

        }
    }

    void dispararAbajo()
    {

        if (disparando == false)
        {
            if (!jugador.RifleDeAsalto)
            {
                disparando = true;
                fogonazoAbajo.SetTrigger("DispararFogonazo");
                Instantiate(prefabBala, PuntoDeDisparoAbajo.position, PuntoDeDisparoAbajo.rotation);
                animator.SetTrigger("DispararAgachado");
                Invoke("TerminaDeDisparar", 0.5f);
            }
            else
            if(!rifleAnimator.GetBool("Disparando"))
            {
                disparando = true;
                fogonazoAbajo.SetBool("DispararFogonazoRifle", true);
                Instantiate(prefabBala, PuntoDeDisparoAbajo.position, PuntoDeDisparoAbajo.rotation);
                Invoke("TerminaDeDisparar", 0.1f);
            }
            else
            {
                rifleAnimator.SetBool("Disparando", false);
                fogonazo.SetBool("DispararFogonazoRifle", false);

            }
        }
    }
}

