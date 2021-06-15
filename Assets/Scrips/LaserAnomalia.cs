using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAnomalia : MonoBehaviour
{
    public float velocidad;
    public bool impactado;
    public Rigidbody2D bala;

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


    void OnTriggerEnter2D(Collider2D _col)
    {
        bala.velocity = transform.right * 0f;
        if (_col.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

    }
}
