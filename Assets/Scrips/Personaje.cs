using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Personaje : MonoBehaviour
{

    public Animator animator;
    public Animator rifleAnimator;
    public SpriteRenderer animatorRenderer;
    public SpriteRenderer rifleRenderer;
    public Collider2D animatorColider;
    public Collider2D rifleColider;

    private bool mirandoDerecha;
    private bool saltando;
    private bool agachado;
    private bool rifleDeAsalto;
    private bool vivo;
    public int salud;
    public bool Vivo
    {
        get { return vivo; }
        set { vivo = value; }
    }
    public bool Agachado
    {
        get { return agachado; }
        set { agachado = value; }
    }
    public bool RifleDeAsalto
    {
        get { return rifleDeAsalto; }
        set { rifleDeAsalto = value; }
    }

    public bool Saltando
    {
        get { return saltando; }
        set { saltando = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        rifleColider.enabled = false;
        animatorColider.enabled = true;
        mirandoDerecha = true;
        agachado = false;
        saltando = false;
        rifleDeAsalto = false;
        rifleAnimator.enabled = false;
        rifleRenderer.enabled = false;
        vivo = true;
        salud = 5;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.Space) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S)
            && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && vivo)
        {
            if (!rifleDeAsalto)
            {
                animatorRenderer.enabled = false;
                animator.enabled = false;
                animatorColider.enabled = false;
                rifleColider.enabled = true;
                rifleAnimator.enabled = true;
                rifleRenderer.enabled = true;
                rifleDeAsalto = true;
            }
            else
            {
                rifleAnimator.enabled = false;
                rifleRenderer.enabled = false;
                rifleColider.enabled = false;
                animatorColider.enabled = true;
                animatorRenderer.enabled = true;
                animator.enabled = true;
                rifleDeAsalto = false;
            }
           
        }


        if (Input.GetKey(KeyCode.A) && !agachado && vivo)
        {
            if (!rifleDeAsalto)
            {
                animator.SetBool("Caminar", true);
                transform.Translate(new Vector3(0.2f, 0.0f));
                float horizontal = -1f;
                girar(horizontal);
            }
            else
            if (!Input.GetKey(KeyCode.UpArrow))
            {
                rifleAnimator.SetBool("Correr", true);
                transform.Translate(new Vector3(0.2f, 0.0f));
                float horizontal = -1f;
                girar(horizontal);
            }
        }
        else
        if (Input.GetKey(KeyCode.D) && !agachado && vivo)
        {
            if (!rifleDeAsalto)
            {
                animator.SetBool("Caminar", true);
                transform.Translate(new Vector3(0.2f, 0.0f));
                float horizontal = 1f;
                girar(horizontal);
            }
            else
            if (!Input.GetKey(KeyCode.UpArrow))
            {
                rifleAnimator.SetBool("Correr", true);
                transform.Translate(new Vector3(0.2f, 0.0f));
                float horizontal = 1f;
                girar(horizontal);
            }
           
        }

        if (Input.GetKey(KeyCode.S) && !Saltando && vivo)
        {
            if (!rifleDeAsalto)
            {
                animator.SetBool("Caminar", false);
                animator.SetBool("Agachado", true);
                agachado = true;
            }
            else
            {
                rifleAnimator.SetBool("Correr", false);
                rifleAnimator.SetBool("Agachado", true);
                agachado = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.S) && !Saltando && vivo)
        {
            if (!rifleDeAsalto)
            {
                animator.SetBool("Agachado", false);
                agachado = false;
            }
            else
            {
                rifleAnimator.SetBool("Agachado", false);
                agachado = false;
            }

        }




        if (Input.GetKeyDown(KeyCode.W) && !Input.GetKey(KeyCode.S) && vivo)
        {
            if (!rifleDeAsalto)
            {

                animator.SetBool("Agachado", false);
                saltar();
            }
            else
            {
                rifleAnimator.SetBool("Agachado", false);
                saltar();
            }
 
        }

        if (animator.GetBool("Caminar") && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !saltando && vivo)
        {
            animator.SetBool("Caminar", false);
        }
        if (rifleAnimator.GetBool("Correr") && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !saltando && vivo)
        {
            rifleAnimator.SetBool("Correr", false);
        }

    }

    void OnCollisionEnter2D(Collision2D _col)
    {
        if (_col.gameObject.tag == "Suelo" && vivo)
        {
            if (!rifleDeAsalto)
            {
                animator.SetBool("Caminar", false);
            }
            else
            {
                rifleAnimator.SetBool("Correr", false);
            }
            saltando = false;
        }


        if (_col.gameObject.tag == "Ataque" && vivo)
        {
            Debug.Log("Ataque recibido salud: "+this.salud);
            salud = salud - 1;
            if (rifleDeAsalto)
            {
                rifleAnimator.SetTrigger("Herido");
            }
            else
            {
                animator.SetTrigger("Herido");
            }

            if(salud <= 0)
            {
                vivo = false;
                if (rifleDeAsalto)
                {
                    rifleAnimator.SetBool("Muerto", true);
                }
                else
                {
                    animator.SetBool("Muerto", true);
                }
                Invoke("MuerteFinal", 0.5f);
            }
        }
    }

    public void MuerteFinal()
    {
        if (rifleDeAsalto)
        {
            rifleAnimator.SetBool("MuerteFinal", true);
        }
        else
        {
            animator.SetBool("MuerteFinal", true);
        }
    }

    public void girar(float horizontal)
    {
        if (horizontal == 1 && !mirandoDerecha || horizontal == -1 && mirandoDerecha && vivo)
        {
            mirandoDerecha = !mirandoDerecha;

            transform.Rotate(0f, 180f, 0f);
        }
    }

    public void saltar()
    {
        if (!saltando && vivo)
        {
            if (!rifleDeAsalto)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, 700.0f));
                animator.SetBool("Caminar", true);
                saltando = true;
            }
            else
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, 700.0f));
                rifleAnimator.SetBool("Correr", true);
                saltando = true;
            }

        }
    }

}
