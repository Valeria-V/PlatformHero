// **** BIBLIOTECAS **** //
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Classe necessaria MonoBehavior
public class PlayerController : MonoBehaviour
{
    // **** VARIAVEIS **** //
    Rigidbody2D rb;

    [SerializeField] Vector2 vel;
    [SerializeField] Vector2 force;
    public Transform floorCollider;


    // Funcao Start - inicio *"Sempre que houver valores de fisica, criar variaveis no Start"
    void Start()
    {  
        rb      = GetComponent<Rigidbody2D>();
        force   = new Vector2(0, 120);
    }

    // Funcao Loop - sempre rodando e atualizando 
    void Update()
    {
        // **** MOVIMENTO **** //
        // Jump
        if (Input.GetButtonDown("Jump") && floorCollider.GetComponent<FloorCollider>().canJump == true)
        {
            rb.velocity = Vector2.zero;
            floorCollider.GetComponent<FloorCollider>().canJump = false;
            rb.AddForce(force);
        }
        vel = new Vector2(Input.GetAxisRaw("Horizontal"), rb.velocity.y);   // Horizontal x1, y-1

    }

    // Funcao FixedUpdate - Nivela FPS para todos sempre atualizara a cada 0.2s
    private void FixedUpdate()
    {
        // Velocidade player nivelada
        rb.velocity = vel;
    }
}
