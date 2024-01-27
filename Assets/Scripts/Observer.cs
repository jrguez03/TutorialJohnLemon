using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;
    public GameEnding gameEnding;
    bool m_IsPlayerInRange;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform == player)
        {
            m_IsPlayerInRange = true;
        }
    }
    //Se crea esta funci�n para que el juego no termine nada m�s la g�rgola te detecte, as� tiene posibilidad de salir del trigger aunque sea detectado.
    void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = false;
        }
    }

    void Update()
    {
        //Crearemos un Ray el cual se utiliza para detectar las colisiones a trav�s de un Collider, a esto se le llama Raycast.
        if(m_IsPlayerInRange)
        {
            //Creamos el nuevo vector para tener la direcci�n del rayo y utilizamos "Vector3.up" que es un atajo para (0, 1, 0).
            Vector3 direction = player.position - transform.position + Vector3.up;
            //Creamos el Ray.
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;
            //Al ser una booleana nuestra variable, necesitamos que nos devuelva la informaci�n y para eso est� el siguiente par�metro: "out".
            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    gameEnding.CaughtPlayer();
                }
            }
        }
    }
}
