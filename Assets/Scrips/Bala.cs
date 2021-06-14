using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float velocidad;
    public bool impactado;
    public Rigidbody2D bala;
    public Animator animator;
    public int daño = 50;


    void Start()
    {
        Invoke("Disparar", 0.1f);
        Invoke("DisparoFallido", 1.5f);
    }

    private void Disparar()
    {
        bala.velocity = transform.right * velocidad;
    }

    private void DisparoFallido()
    {
        Destroy(gameObject);
    }


    void OnTriggerEnter2D (Collider2D _col)
    {
        bala.velocity = transform.right * 0f;
        Zombie zombie =  _col.GetComponent<Zombie>();
        if(zombie != null)
        {
            transform.Rotate(0f, 180f, 0f);
            animator.SetTrigger("EnemigoImpactado");
            zombie.RecibirImpacto(daño);
            Invoke("DestruirBala", 0.5f);
        }
        
    }

    private void DestruirBala()
    {
        Destroy(gameObject);
    }
}

