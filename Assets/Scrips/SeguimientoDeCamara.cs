using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguimientoDeCamara : MonoBehaviour
{

    public Transform jugador;
    public float distanciaDeCamara;

    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<UnityEngine.Camera>().orthographicSize = ((Screen.height / 2) / distanciaDeCamara);    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(jugador.position.x, jugador.position.y+2.5f, transform.position.z);
    }
}
