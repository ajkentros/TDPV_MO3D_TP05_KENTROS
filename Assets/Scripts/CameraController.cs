using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    public Transform transformPlayer;               // variable = transform del Player
    //public float trackingSpeed = 2f;                // variable velocidad de seguimiento
    public Vector2 margin = new Vector2(1, 1);      // varable margen = cu�nto debe alejarse el Player del centro de la pantalla antes de que la c�mara comience a seguirlo en esa direcci�n
    public Vector2 smoothed = new Vector2(3, 3);   // variable para suavizar el movimiento de la c�mara.
    public CompositeCollider2D limitsForeground;    // variable para obtener los l�mites del Foreground

    private Vector3 objetivo;                       // variable para indicar la posici�n a la que debe ir la c�mara

    void Start()
    {
        // verifica si el Player y el l�mite del Foreground est�n asignados en el inspector
        // si no, muestra mensajes de error en la consola
        if (transformPlayer == null)
        {
            transformPlayer = GameObject.FindGameObjectWithTag("Player").transform;

            if (transformPlayer == null)
            {
                Debug.LogError("No hay Player");
            }
        }

        if (limitsForeground == null)
        {
            Debug.LogError("No hay Foreground");
        }
    }

    void LateUpdate()
    {
        // comprueba si el Player y los l�mites del Foreground no son nulos
        if (transformPlayer == null || limitsForeground == null)
            return;

        // define variables que guardan la posici�n del jugador y ala c�mara
        Vector3 positionPlayer = transformPlayer.position;
        Vector3 positionCamera = transform.position;

        // calcula la posici�n objetivo de la c�mara
        Vector3 objetivo = new(positionPlayer.x, positionPlayer.y, positionCamera.z);

        // aplica suavizado al movimiento de la c�mara
        transform.position = Vector3.Lerp(positionCamera, objetivo, smoothed.x * Time.deltaTime);

        // calcula los l�mites del Foreground
        Vector2 minBound = limitsForeground.bounds.min;
        Vector2 maxBound = limitsForeground.bounds.max;

        // limita la c�mara dentro de los l�mites del CompositeCollider2D
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minBound.x, maxBound.x),
            Mathf.Clamp(transform.position.y, minBound.y, maxBound.y),
            transform.position.z);
    }
}