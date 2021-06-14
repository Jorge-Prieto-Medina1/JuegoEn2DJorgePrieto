using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public int salud;
    private bool muerto;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        salud = 100;
        muerto = false;
    }

    // Update is called once per frame
    void Update()
    {
        
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

}
