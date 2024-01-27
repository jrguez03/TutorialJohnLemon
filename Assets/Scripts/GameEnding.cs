using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public GameObject player;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public CanvasGroup caughtBackgroundImageCanvasGroup;

    bool m_IsPlayerAtExit;
    bool m_IsPlayerCaught;
    float m_Timer;

    //Función para que cuando el GameObject entre en contacto con el Trigger, se ejecute la acción.
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            //Lo primero es llamar a la booleana.
            m_IsPlayerAtExit = true;
        }
    }

    public void CaughtPlayer()
    {
        m_IsPlayerCaught = true;
    }

    void Update()
    {
        if(m_IsPlayerAtExit)
        {
            EndLevel(exitBackgroundImageCanvasGroup, false);
        }
        else if (m_IsPlayerCaught)
        {
            EndLevel(caughtBackgroundImageCanvasGroup, true);
        }
    }

    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart)
    {
        m_Timer += Time.deltaTime;

        imageCanvasGroup.alpha = m_Timer / fadeDuration;

        if (m_Timer > fadeDuration + displayImageDuration)
        {
            //Siempre que if sea falso se ejecutará else.
            if(doRestart)
            {
                SceneManager.LoadScene(0);
            }
            else 
            {
            Application.Quit();
            }
        }
    }
}
