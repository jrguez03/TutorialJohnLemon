using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        //LLamamos a la componente Animator de la variable.
        m_Animator = GetComponent<Animator>();
        //LLamamos a la componente Rigidbody
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    //Cambiamos el Update a FixedUpdate porque ahora estoamos utilizando la función OnAnimatorMove, que se ejecuta con físicas.
    void FixedUpdate()
    {
        //Valores de los ejes.
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //De esta forma determinamos en qué ejes se podrá mover el personaje.
        m_Movement.Set(horizontal, 0f, vertical);
        /*Con la línea anterior pasaría una cosa, y es que si el personaje se moviese horizontalmente,
        su velocidad se vería incrementada, así que normalizaremos todos los valores a 1.
        Normalizar un vector significa mantener la misma dirección del vector, pero cambiar su magnitud a 1.*/
        m_Movement.Normalize();

        /*Con estas booleanas podemos saber si están habiendo entradas de teclado tanto en el eje horizontal
        como vertical.*/
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        //Esta línea llama el SetBool Method utilizando la referencia de la componente Animator.
        m_Animator.SetBool("IsWalking", isWalking);

        /*Llamamos a la componente transform de una forma más sencilla y hacemos que la rotación sea fluida en cualquier 
        dispositivo gracias al Time.delTime.*/
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        //Esta línea llama al método LookRotation y crea una rotación hacia la dirección del parámetro dado.
        m_Rotation = Quaternion.LookRotation(desiredForward);
    }

    /*Este método le permite aplicar el movimiento de raíz como desee, lo que significa que el movimiento y
    la rotación se pueden aplicar por separado.*/
    void OnAnimatorMove()
    {
        /*Usamos la variable del Rigidbody para llamar al método MovePosition y pasando un único parámetro, que es su nueva posición.
        La nueva posisción comienza en la actual del RigidBody y luego se le agrega un cambio, 
        el vector de movimiento multiplicado por la magnitud de deltaPosition de la variable Animator.*/
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        //Con esta línea estamos configurando la rotación.
        m_Rigidbody.MoveRotation(m_Rotation);
    }
}
