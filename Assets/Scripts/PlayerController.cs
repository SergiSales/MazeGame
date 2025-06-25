using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerController : MonoBehaviour
{
    public float velocidadMovimiento = 3.0f;

    private Vector3 celdaActual = new Vector3(0, 0, 0);
    
    private void Start() {
        celdaActual = transform.position + new Vector3(0, -0.45f, 0); 
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("hit");
            Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(rayo, out RaycastHit impacto))
            {
                // Se espera que cada celda tenga un script "Celda"
                UnityEngine.Collider celdaClic = impacto.collider;


                if (celdaClic != null && impacto.collider.name == "Floor")
                {
                    Vector3 celdaObjetivo = impacto.point;
                    if (EsCeldaContigua(celdaActual, celdaObjetivo))
                    {
                        StartCoroutine(MoverJugador(impacto.collider.transform.position));

                        celdaActual = celdaObjetivo;
                    }
                }
            }
        }
    }

    // Comprueba que la celda clickeada sea adyacente (solo izquierda, derecha, arriba o abajo)
    private bool EsCeldaContigua(Vector3 actual, Vector3 objetivo)
    {
        float diferenciaX = Math.Abs(actual.x - objetivo.x);
        float diferenciaZ = Math.Abs(actual.z - objetivo.z);

        print("Diferencia X: " + diferenciaX + " Diferencia Z: " + diferenciaZ + " Total: " + (diferenciaX + diferenciaZ));
        // Sumando las diferencias da 1 si es adyacente (no se permiten diagonales)
        return (diferenciaX + diferenciaZ) <= 1.5f;
    }

    // Movimiento inmediato. Si prefieres un movimiento suave, ver el siguiente apartado.
    IEnumerator MoverJugador(Vector3 destino)
    {
        while(Vector3.Distance(transform.position, destino) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destino, velocidadMovimiento * Time.deltaTime);
            yield return null;
        }
        transform.position = destino;
    }


}
