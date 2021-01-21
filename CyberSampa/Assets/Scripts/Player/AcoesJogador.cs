using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;//biblioteca usada para criar UnityEvents

public class AcoesJogador : MonoBehaviour
{
    [Header("MenuPhone")]
    public GameObject phoneObj;
    [Header("Propriedades")]
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
    private float RaioPulo = 0.1f;//define o raio de ação do CheckGound do Player para o pulo
    
    [SerializeField]
    private float ajusteDeColisorAgaixado;
    

    [SerializeField] // para aparecer no inspector do player
    private float velocidade = 4; // defini a velocidade do player
    [Range(10, 22)]
    public float forcapulo = 9.7f;  // defini a força do pulo do player
    private float horizontal;  //variavel para controlar player 1 Eixo X.
    private float velocidadeQueda;

    private bool grounded; //variavel de controle do pulo (condição para pular)

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
    
        ladoDireito = transform.localScale;
        ladoEsquerdo = transform.localScale;
        ladoEsquerdo.x = ladoEsquerdo.x * -1;
    }
    void Update()
    {
       
    }
    private void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, RaioPulo, 1 << LayerMask.NameToLayer("Ground"));
        if (Input.GetKeyDown(pulo) && grounded)
        {
            aoPressionarPulo.Invoke();
        }
        if (Input.GetKey(KeyCode.D)&&grounded)
        {
            transform.position += new Vector3 (Input.GetAxis("Horizontal")*velocidade*Time.deltaTime, 0f, 0f);
            horizontal = 0.5f;
            animator.SetBool("correndo", true);
        }
        if(Input.GetKey(KeyCode.A)&&grounded)
        { 
            transform.position += new Vector3 (Input.GetAxis("Horizontal")*velocidade*Time.deltaTime, 0f, 0f);
            horizontal = -0.5f;
            animator.SetBool("correndo", true);
        }
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)){
            horizontal = 0;
            animator.SetBool("correndo", false);
        }
        if (Input.GetKey(KeyCode.P))
        {
            phoneObj.SetActive(true);
        }

        horizontal = Input.GetAxis("Horizontal");
        MudarDirecao(horizontal);                   //função de direção que recebe o valor do eixo X (-1~1)
        Animacao(horizontal);
        velocidadeQueda = Math.Abs(rb2D.velocity.y); //caso o jogador caia muito rápido ele morrerá, de acordo com o OnCollisionEnter no fim desse código.
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
    private void Animacao(float horizontal)
    {
        animator.SetFloat("velocidade", Mathf.Abs(horizontal));               //Se tiver velocidade abs > 0 anima corrida
    }
    public void Morrer(bool morrer)
    {       
    }
    public void Pulo()//PULO DO PLAYER bool j
    {
        rb2D.AddForce(transform.up * forcapulo, ForceMode2D.Impulse);
    }
    public void AnimacaoPulo(bool jump) //CONTROLE DAS ANIMAÇÕES DO PLAYER
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
    public void Abaixar()
    {   
    }
    void OnDrawGizmos()//desenha a esfera de detecção do chão para o pulo, apenas para visualização
    {                                               
        Gizmos.DrawWireSphere(groundCheck.position, RaioPulo);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
    }
    private void ChamaMorte()
    {
    }
}
