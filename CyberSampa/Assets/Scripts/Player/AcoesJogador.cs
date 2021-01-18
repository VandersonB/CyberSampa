using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;//biblioteca usada para criar UnityEvents

public class AcoesJogador : MonoBehaviour
{
     
    
    private Rigidbody2D rb2D; //criação de variável de manipulação do rigidbody do player
    private Animator animator; //criação de variavel de manipulaçao do animator
    private AudioSource meuAudioSource;
    private Collider2D colisorPe;
    public Vector2 posicaoInicial { get; private set; }
    [SerializeField]
    private Transform groundCheck; //definida pela posição do GameObject "GroundCheck" do Player,
    [SerializeField]
    private AudioClip audioPulo;
    [SerializeField]
    private AudioClip audioMorte;
    [SerializeField]
    private KeyCode pulo;
    [SerializeField]
    private UnityEvent aoPressionarPulo;
    
    [SerializeField]
    private float RaioPulo = 0.3f;//define o raio de ação do CheckGound do Player para o pulo
    [SerializeField]
    private float ajusteDeColisorAgaixado;
    [SerializeField] // para aparecer no inspector do player
    private float velocidade; // defini a velocidade do player
    [Range(10, 40)]
    public float forcapulo;  // defini a força do pulo do player
    private float horizontal;  //variavel para controlar player 1 Eixo X

    public bool grounded; //variavel de controle do pulo (condição para pular)
    public bool podeDash;
    public bool dashando;
    private float tempoDash;
    public float duracaoDash;
    public float velocidadeDash;

    private Vector3 ladoDireito;
    private Vector3 ladoEsquerdo;


    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();         //Coleta os componentes do player
        animator = GetComponent<Animator>();        // --
        meuAudioSource = this.GetComponent<AudioSource>();
        colisorPe = this.GetComponent<CapsuleCollider2D>();
        posicaoInicial = this.GetComponent<Transform>().position;   
    }

    private void Start()
    {
        podeDash = true;
        forcapulo = 28f;
        velocidade = 8;
        duracaoDash = 0.5f;
        tempoDash = duracaoDash;
        velocidadeDash = 2 * velocidade;
        ladoDireito = transform.localScale;
        ladoEsquerdo = transform.localScale;
        ladoEsquerdo.x = ladoEsquerdo.x * -1;
    }
    void Update()
    {
       
    }
    private void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        Chao();
        Correr();
        Pulo();
        Dash();
        MudarDirecao(horizontal);                   //função de direção que recebe o valor do eixo X (-1~1)
        AnimacaoCorrer(horizontal);
        AnimacaoPulo(grounded);
        AnimacaoDash(dashando);
    }

    private void Chao()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, RaioPulo, 1 << LayerMask.NameToLayer("Ground"));
    }
    private void Correr()
    {
        
        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("ANDOU PRA DIREITA!");
            transform.position += new Vector3(Input.GetAxis("Horizontal") * velocidade * Time.deltaTime, 0f, 0f);
            horizontal = 0.5f;
            if (grounded)
            {
                animator.SetBool("correndo", true);
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("ANDOU PRA ESQUERDA!");
            transform.position += new Vector3(Input.GetAxis("Horizontal") * velocidade * Time.deltaTime, 0f, 0f);
            horizontal = -0.5f;
            if (grounded)
            {
                animator.SetBool("correndo", true);
            }
        }
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            Debug.Log("PAROU!");
            horizontal = 0;
            animator.SetBool("correndo", false);
        }
    }
    private void Pulo()//PULO DO PLAYER bool j
    {
        if (Input.GetKeyDown(KeyCode.W) && grounded)
        {
            Debug.Log("PULOU!");
            rb2D.AddForce(transform.up * forcapulo, ForceMode2D.Impulse);
        }
        
    }
    private void Dash()
    {
        if(Input.GetKeyDown(KeyCode.S) && grounded && podeDash)
        {
            if(tempoDash <= 0)
            {
                rb2D.velocity = Vector2.zero;
                tempoDash = duracaoDash;
                podeDash = false;
                dashando = false;
            }
            else
            {
                dashando = true;
                tempoDash = -Time.deltaTime;
                if (horizontal > 0)
                {
                    rb2D.velocity = Vector2.right * velocidadeDash;
                }
                if (horizontal < 0)
                {
                    rb2D.velocity = Vector2.left * velocidadeDash;
                }
            }
            if (horizontal > 0)
            {
                rb2D.velocity = Vector2.right * velocidadeDash;
            }
            if(horizontal < 0)
            {
                rb2D.velocity = Vector2.left * velocidadeDash;
            }
        }
        if(Input.GetKeyUp(KeyCode.S))
        {
            podeDash = true;
            dashando = false;
            tempoDash = duracaoDash;
        }
    }
    private void MudarDirecao(float horizontal) //VIRADA DE SPRITE DO PLAYER
    {
        if (horizontal > 0)
        {
            transform.localScale = ladoDireito;
        }
        if(horizontal < 0)
        {
            transform.localScale = ladoEsquerdo;
        }
    }
    private void AnimacaoCorrer(float horizontal)
    {
        animator.SetFloat("velocidade", Mathf.Abs(horizontal));               //Se tiver velocidade abs > 0 anima corrida
    }
    private void AnimacaoPulo(bool jump) //CONTROLE DAS ANIMAÇÕES DO PLAYER
    {
        if (!jump)
        {
            animator.SetBool("pulando", true);
        }            //Se não estiver no chão anima o pulo
        if (jump)
        {
            animator.SetBool("pulando", false);
        }          // --- situação oposta ...,
    }
    private void AnimacaoDash(bool dash)
    {
        if(dash)
        {
            animator.SetBool("dashando", true);
        }
        if (!dash)
        {
            animator.SetBool("dashando", false);
        }


    }



    void OnDrawGizmos()//desenha a esfera de detecção do chão para o pulo, apenas para visualização
    {                                               
        Gizmos.DrawWireSphere(groundCheck.position, RaioPulo);
    }
    private void OnCollisionEnter2D(Collision2D collision2D)
    {           //Detecta se colidiu
        //Debug.Log("COLIDIU com " + collision2D.gameObject.tag);
    }
    private void OnCollisionExit2D(Collision2D collision2D)
    {           // Detecta se parou de colidir
        //Debug.Log("PAROU DE COLIDIR com " + collision2D.gameObject.tag);
    }
}
