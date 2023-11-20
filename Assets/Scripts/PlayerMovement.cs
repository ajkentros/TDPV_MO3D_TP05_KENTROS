using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float velocityPlayer;        // variable = velocidad del player para moverse
    public float jumpForcePlayer;       // variable = fuerza del salto
    public float jumpMaxPlayer;         // máxima = cantidad de saltos
    public LayerMask layerForeground;   // variable para la capa del Foreground = suelo

    private Rigidbody2D rigidBodyPlayer;        //variable Rigidbody del Player
    private BoxCollider2D boxColliderPlayer;    //variable Collider del player
    private float jumpNotMadePlayer;            //saltos restantes que no se hicieron

    private void Start()
    {
        // inicializa el Rigidbody2D, el BoxCollider del Player
        // inicializa los saltos no realizados por el Player = a la cantidad máxima de saltos asignados que puede realizar 
        rigidBodyPlayer = GetComponent<Rigidbody2D>();
        boxColliderPlayer = GetComponent<BoxCollider2D>();
        jumpNotMadePlayer = jumpMaxPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        // llama a los métodos que gestionan el movimiento y salto del Player
        ProcessMovement();
        ProcessJump();
    }

    bool OnTheForegronud()
    {
        // calcula el Raycast del Player desde el centro del boxColliderPlayer hacia abajo en Y para verificar si el Player está en el suelo o en un objeto del layer especificado (layerForeground)
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxColliderPlayer.bounds.center, new Vector2(boxColliderPlayer.bounds.size.x, boxColliderPlayer.bounds.size.y), 0f, Vector2.down, 0.2f, layerForeground);

        // retorna true cuando el player si el Raycast golpea algo, indicando que el Player está en el suelo, sino devuelve false
        return raycastHit.collider != null;
    }

    void ProcessJump()
    {
        // si el Player está en el piso => la cantidad de saltos que no se hicieron = cantidad máxima de saltos asignados
        if (OnTheForegronud())
        {
            jumpNotMadePlayer = jumpMaxPlayer;
        }

        // si se hace clic en la tecla space y la cantida de saltos no realizados >0 => se resta 1 saltop a los no ralizados, se mueve el Rigibody de Player a la velocidad vertical en cero y aplica una fuerza hacia arriba
        if (Input.GetKeyDown(KeyCode.Space) && jumpNotMadePlayer > 0)
        {
            jumpNotMadePlayer--;
            rigidBodyPlayer.velocity = new Vector2(rigidBodyPlayer.velocity.x, 0f);
            rigidBodyPlayer.AddForce(Vector2.up * jumpForcePlayer, ForceMode2D.Impulse);
        }
    }

    void ProcessMovement()
    {
        // obtiene la entrada del eje horizontal (Input.GetAxis("Horizontal")) para determinar la dirección del movimiento del Player 
        float inputMovimiento = Input.GetAxis("Horizontal");

        // establece la velocidad horizontal del rigidBodyPlayer según la entrada del jugador y la velocidad máxima (velocityPlayer)
        rigidBodyPlayer.velocity = new Vector2(inputMovimiento * velocityPlayer, rigidBodyPlayer.velocity.y);

        
    }

  
}
