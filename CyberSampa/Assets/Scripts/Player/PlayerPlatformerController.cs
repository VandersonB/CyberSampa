using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject
{
    [SerializeField]
    private KeyCode dash;
    [SerializeField]
    private KeyCode correr;
    public static PlayerPlatformerController instance;
    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Vector2 move;
    // Use this for initialization
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        instance = this;
    }

    //método que computa as velocidades do mplayer baseado nos inputs
    //Nesse método abaixo constam as seguintes mecânicas: Andar, pular, Dash (fazer), agachar (fazer);
    protected override void ComputeInputs()
    {
        move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        //CORRENDO____________________________________________
        if (maxSpeed == 7 && Input.GetKey(correr)) 
        {
            Correr();         
        }
        else
        {
            maxSpeed = 7;
        }
        //DASH
        if (Input.GetKeyUp(dash))
        {
            tempoAtualDash = 0; //reseta o tempo de dash para iniciar a execução
        }
        //Pulando____________________________________________
        if (Input.GetButtonDown("Jump") && grounded)
        {
            Pulo();
        }

        FlipSprite(); //Verificador de Sprite para virar o lado
        SetAnimacoes(); //inicia as animações
        targetVelocity = move * maxSpeed; //retorna a velocidade de acordo com os comandos de dash/correr/andar
    }


    void Pulo()
    {
        velocity.y = jumpTakeOffSpeed; //faz o papel do impulso "rb2D.AddForce(transform.up * forcapulo, ForceMode2D.Impulse);"

        if (Input.GetButtonUp("Jump")) //se ele já estiver fora do chão, não executa nada (pode ser usado para criar condição do wallJump ou pulo duplo)
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
            }
        } 
    }

    void Correr()
    {
        maxSpeed = 14;
    }

    void FlipSprite()
    {
        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }
         
    void SetAnimacoes()
    {
        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);
    }

 }
