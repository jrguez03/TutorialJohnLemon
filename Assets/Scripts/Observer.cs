using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class Observer : MonoBehaviour
{
    [SerializeField] GameObject m_Exclamation;
    public Transform player;
    public GameEnding gameEnding;
    public AudioSource exclamationAudio;

    bool m_IsPlayerInRange;
    float cD_Timer = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform == player)
        {
            exclamationAudio.Play();
            m_Exclamation.SetActive(true);
            m_IsPlayerInRange = true;
        }
    }
    //Se crea esta función para que el juego no termine nada más la gárgola te detecte, así tiene posibilidad de salir del trigger aunque sea detectado.
    void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            m_Exclamation.SetActive(false);
            m_IsPlayerInRange = false;
            ResetTimer();
        }
    }

    private void Start()
    {
        m_Exclamation.SetActive(false);
    }

    public void ResetTimer()
    {
        cD_Timer = 0f;
    }

    void Update()
    {
        //Crearemos un Ray el cual se utiliza para detectar las colisiones a través de un Collider, a esto se le llama Raycast.
        if(m_IsPlayerInRange)
        {
            //Creamos el nuevo vector para tener la dirección del rayo y utilizamos "Vector3.up" que es un atajo para (0, 1, 0).
            Vector3 direction = player.position - transform.position + Vector3.up;
            //Creamos el Ray.
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;
            //Al ser una booleana nuestra variable, necesitamos que nos devuelva la información y para eso está el siguiente parámetro: "out".
            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    cD_Timer += Time.deltaTime;
                    if (cD_Timer >= 2f)
                    {
                        gameEnding.CaughtPlayer();
                    }
                }
            }
        }
    }
}
